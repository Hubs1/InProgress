using System.Web;
using System.Web.Optimization;

namespace MillimanZior
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            /*bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
             bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));*/

            // creating bundles for adding datatable and bootstrap theme
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/themes/bootstrap-sb-admin-2/vendor/bootstrap/js/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapDatatables").Include(
                "~/Content/themes/bootstrap-sb-admin-2/vendor/datatables/dataTables.bootstrap4.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/dataTables/datatables.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryDatatables").Include(
                "~/Content/themes/bootstrap-sb-admin-2/vendor/datatables/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrapDatatablesCss").Include(
                "~/Content/themes/bootstrap-sb-admin-2/vendor/datatables/dataTables.bootstrap4.css"));
            //"~/Content/dataTables/dataTables.bootstrap.css"));

        }
    }
}
