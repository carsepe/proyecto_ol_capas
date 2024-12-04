using Domain.Model;
using Infraestructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal ModelContext context;
    internal DbSet<TEntity> dbSet;

    public Repository(ModelContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public ModelContext GetDbContext()
    {
        return this.context;
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = this.dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = this.dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await this.dbSet.Where(expression).ToListAsync();
    }

    public virtual TEntity FindSingleBy(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter != null ? this.dbSet.Where(filter).SingleOrDefault()! : throw new InvalidOperationException("Filter cannot be null");
    }

    public virtual IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter != null ? this.dbSet.Where(filter) : Enumerable.Empty<TEntity>();
    }

    public virtual TEntity GetByID(object id)
    {
        return this.dbSet.Find(id) ?? throw new InvalidOperationException($"Entity with id {id} not found.");
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await this.dbSet.FindAsync(id) ?? throw new InvalidOperationException($"Entity with id {id} not found.");
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this.dbSet.ToListAsync();
    }

    public virtual void Insert(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        this.dbSet.Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await AddAsync(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await this.dbSet.AddAsync(entity);
        await this.context.SaveChangesAsync();
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        this.dbSet.RemoveRange(entities);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = this.dbSet.Find(id);
        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

        if (this.context.Entry(entityToDelete).State == EntityState.Detached)
        {
            this.dbSet.Attach(entityToDelete);
        }

        this.dbSet.Remove(entityToDelete);
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await this.dbSet.FindAsync(id);
        if (entity != null)
        {
            this.dbSet.Remove(entity);
            await this.context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        this.dbSet.Remove(entity);
        await this.context.SaveChangesAsync();
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        if (entityToUpdate == null) throw new ArgumentNullException(nameof(entityToUpdate));

        this.dbSet.Update(entityToUpdate);
        this.context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void Update(object id, TEntity entityToUpdate)
    {
        if (entityToUpdate == null) throw new ArgumentNullException(nameof(entityToUpdate));

        var entity = this.dbSet.Find(id);
        if (entity == null)
        {
            throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        this.context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        this.context.Entry(entity).State = EntityState.Modified;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        this.dbSet.Update(entity);
        await this.context.SaveChangesAsync();
    }

    public virtual IEnumerable<TEntity> StoreProcedure(string nameProcedure, object? parameters = null)
    {
        List<SqlParameter> parms = new();
        if (parameters != null)
        {
            foreach (var item in parameters.GetType().GetProperties())
            {
                nameProcedure += $" @{item.Name},";
                parms.Add(new SqlParameter { ParameterName = $"@{item.Name}", Value = item.GetValue(parameters, null) ?? "" });
            }

            nameProcedure = nameProcedure.TrimEnd(',');
        }

        return this.context.Set<TEntity>()
               .FromSqlRaw<TEntity>(nameProcedure, parms.ToArray())
               .AsEnumerable()!;
    }
}
