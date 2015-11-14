using Geocaching.Exercise;
using Geocaching.Exercise.Data;
using Geocaching.Exercise.Data.Entities;
using System;
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
        [ResponseType(typeof(ApiResult<Geocache[]>))]
        public async Task<IHttpActionResult> Get()
        {
            var result = new ApiResult<Geocache[]>();

            try
            {
                using (var work = new GeocachingWork())
                {
                    var repository = work.GetRepository<Geocache>();

                    var entities = await repository.RetrieveAsync();
                    if (null == entities)
                    {
                        result.Message = "No Geocaches found.";
                    }
                    else
                    {
                        result.Results = entities.OrderBy(e => e.Name).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.GetDetailMessage();
            }

            return this.Ok(result);
        }

        [HttpGet]
        [Route("api/geocache/{id}")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = new ApiResult<Geocache>();

            try
            {
                using (var work = new GeocachingWork())
                {
                    var repository = work.GetRepository<Geocache>();

                    var spec = new DataRetrievalSpecification<Geocache>();
                    spec.Filter = x => x.Id == id;

                    var entity = await repository.RetrieveFirstAsync(spec);
                    if (null == entity)
                    {
                        result.Message = string.Format("No Geocache found. Id: {0}", id);
                    }
                    else
                    {
                        result.Results = entity;
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.GetDetailMessage();
            }

            return this.Ok(result);
        }

        [HttpPost]
        [Route("api/geocache")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Create(Geocache newValue)
        {
            var result = new ApiResult<Geocache>();

            try
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

                result.Results = newValue;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.GetDetailMessage();
            }

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("api/geocache/{id}")]
        [ResponseType(typeof(Geocache))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = new ApiResult<Geocache>();

            try
            {
                using (var work = new GeocachingWork())
                {
                    var repository = work.GetRepository<Geocache>();

                    var spec = new DataRetrievalSpecification<Geocache>();
                    spec.Filter = x => x.Id == id;

                    var cache = await repository.RetrieveFirstAsync(spec);
                    if (null == cache)
                    {
                        result.Message = string.Format("No Geocache found. Id: {0}", id);
                    }
                    else
                    {
                        repository.Delete(cache);
                        await work.CommitAsync();

                        result.Results = cache;
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.GetDetailMessage();
            }

            return this.Ok(result);
        }
    }
}
