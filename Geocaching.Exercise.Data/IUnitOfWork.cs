using System;
using System.Threading.Tasks;

namespace Geocaching.Exercise.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    }
}
