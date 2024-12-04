using Domain.Model;

namespace Infraestructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        public bool Save();

        public ModelContext Context { get; }

        Task<int> CompleteAsync();
    }
}