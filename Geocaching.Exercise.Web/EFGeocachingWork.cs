using Geocaching.Exercise.Data.Model.EF;

namespace Geocaching.Exercise.Web
{
    public class EFGeocachingWork : UnitOfWork
    {
        public EFGeocachingWork()
            : base(() => new GeocachingEntities())
        {
        }
    }
}