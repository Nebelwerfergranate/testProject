using System.Web;
using System.Web.Optimization;

namespace Sandbox_Products
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/lodash.js",
                "~/Scripts/jquery.bootstrap-growl.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.jqGrid.js",
                "~/Scripts/i18n/grid.locale-en.js",
                "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/core")
                .IncludeDirectory("~/Scripts/Core", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/resources")
                .IncludeDirectory("~/Scripts/Resources", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/components")
                .IncludeDirectory("~/Scripts/Components", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css",
                      "~/Content/bootstrap-datetimepicker.css")
                      .IncludeDirectory("~/Content/Css", "*.css", true));          
        }
    }
}
