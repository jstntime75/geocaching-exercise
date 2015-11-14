using System;
using System.Linq;
using System.Linq.Expressions;

namespace Geocaching.Exercise.Data
{
    public class DataRetrievalSpecification<TEntity>
        where TEntity : Entity
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
