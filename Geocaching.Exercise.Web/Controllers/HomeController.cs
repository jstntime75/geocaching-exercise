using System.Web.Mvc;

namespace Geocaching.Exercise.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Geocaching Exercise";

            return View();
        }
    }
}
