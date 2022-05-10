using System.Web;
using System.Web.Optimization;

namespace SCA
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/plantilla").Include(
                        "~/Content/lib/easing/easing.min.js",
                        "~/Content/lib/waypoints/waypoints.min.js",
                        "~/Content/lib/owlcarousel/owl.carousel.min.js",
                        "~/Content/lib/tempusdominus/js/moment.min.js",
                        "~/Content/lib/tempusdominus/js/moment-timezone.min.js",
                        "~/Content/lib/tempusdominus/js/tempusdominus-bootstrap-4.min.js",
                        "~/Content/lib/isotope/isotope.pkgd.min.js",
                        //"~/Content/lib/lightbox/js/lightbox.min.js",
                        "~/Content/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/img/favicon.ico",
                     "~/Content/lib/owlcarousel/assets/owl.carousel.min.css",
                     "~/Content/lib/tempusdominus/css/tempusdominus-bootstrap-4.min.css",
                     //"~/Content/lib/lightbox/css/lightbox.min.css",
                     "~/Content/css/bootstrap.min.css",
                     "~/Content/css/style.css"));
        }
    }
}
