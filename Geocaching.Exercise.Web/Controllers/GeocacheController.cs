using Geocaching.Exercise.Data;
using Geocaching.Exercise.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Geocaching.Exercise.Web.Controllers
{
    public class GeocacheController : ApiController
    {
        [HttpGet]
        [Route("api/geocache")]
        [ResponseType(typeof(Geocache[]))]
        public async Task<IHttpActionResult> Get()
        {
            using (var work = new GeocachingWork())
            {
                var repository = work.GetRepository<Geocache>();

                var entities = await repository.RetrieveAsync();
                if (null == entities)
                {
                    return this.NotFound();
                }

                return this.Ok(entities.OrderBy(e => e.Name).ToArray());
            }
        }

        [HttpGet]
        [Route("api/geocache/{id}")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Get(int id)
        {
            using (var work = new GeocachingWork())
            {
                var repository = work.GetRepository<Geocache>();

                var spec = new DataRetrievalSpecification<Geocache>();
                spec.Filter = x => x.Id == id;

                var entity = await repository.RetrieveFirstAsync(spec);
                if (null == entity)
                {
                    return this.NotFound();
                }

                return this.Ok(entity);
            }
        }

        [HttpPost]
        [Route("api/geocache")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Create(Geocache newValue)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            using (var work = new GeocachingWork())
            {
                var repository = work.GetRepository<Geocache>();

                repository.Create(newValue);
                await work.CommitAsync();
            }

            return this.Ok(newValue);
        }

        [HttpDelete]
        [Route("api/geocache/{id}")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var work = new GeocachingWork())
            {
                var repository = work.GetRepository<Geocache>();

                var spec = new DataRetrievalSpecification<Geocache>();
                spec.Filter = x => x.Id == id;

                var cache = await repository.RetrieveFirstAsync(spec);
                if (null != cache)
                {
                    repository.Delete(cache);
                    await work.CommitAsync();
                }

                return this.Ok(cache);
            }
        }
    }
}
