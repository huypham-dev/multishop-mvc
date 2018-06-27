using System.Web;
using System.Web.Optimization;

namespace Shop_Ban_Hang
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/Scripts/jquery-ui.js",
                       "~/Content/Scripts/jquery-ui.min.js"
                        ));
			//admin
			bundles.Add(new ScriptBundle("~/bundles/jquery/Admin").Include(
						"~/Scripts/jquery-{version}.js",
							"~/Areas/Admin/Scripts/DataTables/jquery.js",
						"~/Areas/Admin/Scripts/DataTables/datatable.js"
					

						));


			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
					  "~/Content/css/bootstrap.css",
					  "~/Content/css/nn-style.css",
                      "~/Content/site.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/jquery-ui.min.css"

                      ));

			//admin
			bundles.Add(new StyleBundle("~/Areas/Admin/Scripts/DataTables").Include(
					 "~/Areas/Admin/Scripts/DataTables/css/datatable.css"

					 ));

		}
    }
}
