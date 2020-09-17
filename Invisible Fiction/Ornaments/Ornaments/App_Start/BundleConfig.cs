using System.Web;
using System.Web.Optimization;

namespace Ornaments
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                     "~/Content/Theme/plugins/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Content/Theme/plugins/jqvmap/jquery.vmap.min.js",
                     "~/Content/Theme/plugins/jqvmap/maps/jquery.vmap.usa.js",
                     "~/Content/Theme/plugins/moment/moment.min.js",
                     "~/Content/Theme/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                     "~/Content/Theme/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",

                     "~/Scripts/JS/adminlte.js",
                     "~/Scripts/custom-script.js",
                     "~/Scripts/sweetalert-IE-support.js",
                     "~/Scripts/sweetalert2.min.js",

                     "~/Content/Theme/plugins/datatables/jquery.dataTables.min.js",
                     "~/Content/Theme/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                     "~/Content/Theme/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                     "~/Content/Theme/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                     "~/Content/Theme/plugins/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Content/Theme/plugins/jqvmap/jquery.vmap.min.js",
                     "~/Content/Theme/plugins/jqvmap/maps/jquery.vmap.usa.js",
                     "~/Content/Theme/plugins/moment/moment.min.js",
                     "~/Content/Theme/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",

                     "~/Scripts/JS/adminlte.js",
                     "~/Scripts/custom-script.js"));

            bundles.Add(new ScriptBundle("~/bundles/General").Include(
                    "~/Content/Theme/plugins/jquery/jquery.min.js",
                     "~/Content/Theme/plugins/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Scripts/JS/adminlte.js",
                     "~/Scripts/custom-script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Theme/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/Theme/plugins/moment/moment.min.js",
                      "~/Content/Theme/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                     "~/Content/Theme/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                     "~/Content/Theme/plugins/jqvmap/jqvmap.min.css",
                      "~/Content/Theme/css/adminlte.min.css",
                      "~/Content/Theme/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                      "~/Content/Theme/custom-style.css",
                      "~/Content/Theme/sweetalert2.min.css"
                      ));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/Theme/css/adminlte.min.css",
            //          "~/Content/Theme/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
            //          "~/Content/Theme/plugins/daterangepicker/daterangepicker.css",
            //          "~/Content/Theme/plugins/summernote/summernote-bs4.min.css",
            //          "~/Content/Theme/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
            //          "~/Content/Theme/plugins/fontawesome-free/css/all.min.css",
            //          "~/Content/Theme/plugins/jqvmap/jqvmap.min.css"
            //          ));
        }
    }
}
