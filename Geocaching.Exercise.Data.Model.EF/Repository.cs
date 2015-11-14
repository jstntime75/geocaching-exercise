using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Geocaching.Exercise.Data.Model.EF
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private string _entitySetName;

        public Repository(GeocachingDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.Context = context;
        }

        public GeocachingDbContext Context { get; private set; }

        public string EntitySetName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_entitySetName))
                {
                    var pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));
                    _entitySetName = pluralizer.Pluralize(typeof(TEntity).Name);
                }

                return _entitySetName;
            }
        }

        public void Create(TEntity entity)
        {
            this.Context.ChangeState(entity, EntityState.Added);
        }

        public async Task<TEntity> RetrieveFirstAsync(DataRetrievalSpecification<TEntity> specification = null)
        {
            return await GetQuery(specification).FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> RetrieveAsync(DataRetrievalSpecification<TEntity> specification = null)
        {
            return await GetQuery(specification).ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            this.Context.ChangeState(entity, EntityState.Deleted);
        }

        private IQueryable<TEntity> GetQuery(DataRetrievalSpecification<TEntity> specification)
        {
            var query = this.Context.GetEntitySet<TEntity>().AsQueryable();

            if (null == specification)
            {
                return query;
            }

            if (null != specification.Filter)
            {
                query = query.Where(specification.Filter);
            }

            if (null != specification.Skip && specification.Skip > 0)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (null != specification.Take && specification.Take > 0)
            {
                query = query.Take(specification.Take.Value);
            }

            return query;
        }
    }
}
