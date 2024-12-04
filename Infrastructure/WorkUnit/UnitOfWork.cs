using Domain.Model;
using Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.WorkUnit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool _disposed = false;

        public UnitOfWork(ModelContext context)
        {
            _context = context;
        }

        public ModelContext Context => _context;

        public ModelContext GetDbContext()
        {
            return _context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)]!;
            }

            IRepository<T> repo = new Repository<T>(_context);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public bool Save()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }

        public async Task<bool> SaveAsync()
        {
            bool returnValue = true;
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception)
                {
                    returnValue = false;
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return returnValue;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
