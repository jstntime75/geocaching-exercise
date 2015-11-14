using Geocaching.Exercise.Data.Model.EF;

namespace Geocaching.Exercise.Web
{
    public class GeocachingWork : UnitOfWork
    {
        public GeocachingWork()
            : base(() => new GeocachingEntities())
        {
        }
    }
}