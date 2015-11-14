using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocaching.Exercise.Data.Model.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly GeocachingDbContext _context;

        public UnitOfWork(Func<GeocachingDbContext> contextBuilder)
        {
            if (contextBuilder == null)
            {
                throw new ArgumentNullException("contextBuilder");
            }

            this._context = contextBuilder();

            if (_context == null)
            {
                throw new NotSupportedException("context cannot be null");
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
