using System.Web;
using System.Web.Optimization;

namespace Geocaching.Exercise.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/angular-toastr.css"));

            bundles.Add(new ScriptBundle("~/Scripts/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-aria.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/angular-toastr.tpls.js"));

            bundles.Add(new ScriptBundle("~/Scripts/libraries").Include(
                "~/Scripts/lodash.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/angular-app").IncludeDirectory("~/Scripts/app/", "*.js", true));
        }
    }
}
