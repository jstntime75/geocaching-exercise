using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Geocaching.Exercise.Data.Model.EF
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "dispose is implemented in base class")]
    public abstract class GeocachingDbContext : DbContext
    {
        #region constructors

        public GeocachingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public GeocachingDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "variable namine follows base class pattern")]
        public GeocachingDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        public GeocachingDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public GeocachingDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected GeocachingDbContext()
            : base()
        {
        }

        protected GeocachingDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        #endregion

        public void ChangeState<T>(T entity, EntityState state) where T : class
        {
            Entry<T>(entity).State = state;
        }

        public IDbSet<T> GetEntitySet<T>() where T : class
        {
            return this.Set<T>();
        }

        public virtual int Commit()
        {
            if (this.ChangeTracker.Entries().Any(IsChanged))
            {
                return this.SaveChanges();
            }

            return 0;
        }

        private static bool IsChanged(DbEntityEntry entity)
        {
            return IsStateEqual(entity, EntityState.Added) ||
                   IsStateEqual(entity, EntityState.Deleted) ||
                   IsStateEqual(entity, EntityState.Modified);
        }

        private static bool IsStateEqual(DbEntityEntry entity, EntityState state)
        {
            return (entity.State & state) == state;
        }
    }
}
