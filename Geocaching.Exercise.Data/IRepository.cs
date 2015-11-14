using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geocaching.Exercise.Data
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        string EntitySetName { get; }

        void Create(TEntity entity);

        Task<TEntity> RetrieveFirstAsync(DataRetrievalSpecification<TEntity> specification = null);

        Task<IList<TEntity>> RetrieveAsync(DataRetrievalSpecification<TEntity> specification = null);

        void Delete(TEntity entity);
    }
}
