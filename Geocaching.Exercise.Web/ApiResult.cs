using Geocaching.Exercise.Data;

namespace Geocaching.Exercise.Web
{
    public class ApiResult<T>
    {
        public T Results { get; set; }

        public bool HasError { get; set; }

        public string Message { get; set; }
    }
}