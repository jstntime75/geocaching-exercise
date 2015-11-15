using Geocaching.Exercise.Data;
using System.Web.Http;

namespace Geocaching.Exercise.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected IUnitOfWork GetWork()
        {
            return new EFGeocachingWork();
        }
    }
}