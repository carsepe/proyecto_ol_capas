using Domain.Model;
using System.Linq.Expressions;

namespace Infraestructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Métodos para obtener datos
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);

        TEntity FindSingleBy(Expression<Func<TEntity, bool>>? filter = null);

        IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>>? filter = null);

        TEntity GetByID(object id);

        Task<TEntity> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        // Métodos de inserción
        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        Task AddAsync(TEntity entity);

        // Métodos de eliminación
        void Delete(object id);

        void Delete(TEntity entityToDelete);

        Task DeleteAsync(object id);

        Task DeleteAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        // Métodos de actualización
        void Update(TEntity entityToUpdate);

        void Update(object id, TEntity entityToUpdate);

        Task UpdateAsync(TEntity entity);

        // Procedimientos almacenados
        IEnumerable<TEntity> StoreProcedure(string nameProcedure, object? parameters = null);

        // Obtener contexto de base de datos
        ModelContext GetDbContext();
    }
}
