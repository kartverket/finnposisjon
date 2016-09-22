using System.Web;
using System.Web.Optimization;

namespace Kartverket.FinnPosisjon
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/assets/css/main.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
               "~/Content/assets/js/main.min.js"
           ));
        }
    }
}
