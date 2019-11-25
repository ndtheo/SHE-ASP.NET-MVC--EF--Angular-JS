#region Using Directives

using System.Web.Optimization;

#endregion

namespace WebApplication
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
#if !DEBUG
            BundleTable.EnableOptimizations = true;


            //#endif
            bundles.Add(new ScriptBundle("~/bundles/TestApp")
            .IncludeDirectory("~/Scripts/Controllers", "*.js")
            .Include("~/Scripts/TestApp.js"));

            // For visual studio generated scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.1.1.min.js"));


#endif

 //< script src = "http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.6.2/modernizr.min.js" ></ script >
 
 //  < script src = "http:////cdnjs.cloudflare.com/ajax/libs/jquery/1.9.1/jquery.min.js" ></ script >
  
 //   < script src = "http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.3.1/js/bootstrap.min.js" ></ script >
               // Use the development version of Modernizr to develop with and learn from. Then, when you're
               // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
               bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-2.8.3.min.js"));

            // For visual studio generated scripts
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //	"~/Scripts/jquery-{version}.js"));
            // For visual studio generated scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.1.1.min.js"));

            ///    $.noConflict();
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.min.js",
				"~/Scripts/respond.min.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/bootstrap.min.css",
				"~/Content/Site/Site.css",
				"~/Content/Site/Tables.css",
				"~/Content/Site/Forms.css",
				"~/Content/Site/Modals.css",
				"~/Content/Site/PathsFix.css",
				"~/Content/Site/ui-select-override.css"));

			bundles.Add(new StyleBundle("~/Content/angularCss").Include(
				"~/Scripts/QuantumUI-1.0.0/css/bootstrap-quantumui.css",
				"~/Scripts/QuantumUI-1.0.0/css/quantumui.css",
				"~/Scripts/QuantumUI-1.0.0/css/addon/effect-light.min.css",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt.css",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt-tree-plugin.css",
				"~/Scripts/angular-ui-tree-v2.15.0/angular-ui-tree.min.css",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt-progress-plugin.css",
				"~/Scripts/angular-ui/ui-select/select.css"
			));

			bundles.Add(new ScriptBundle("~/bundles/angularLib").Include(
				"~/Scripts/angular.js",
				"~/Scripts/i18n/angular-locale_el-gr.js",
				"~/Scripts/angular-route.js",
				"~/Scripts/angular-sanitize.js",
				"~/Scripts/angular-animate.js",
				"~/Scripts/angular-ui/ui-bootstrap-tpls.js", //This is the one containing the modal window and other templates(tpls)
				"~/Scripts/angular-ui/ui-select/select.js",
				"~/Scripts/angular-ui/ui-select/overrideMultipleTemplate.js", // Custom script that overrides a template, and fixes the change line error of the default templete
				"~/Scripts/QuantumUI-1.0.0/js/quantumui-all.js",
				"~/Scripts/moment.min.js",
				"~/Scripts/angular-moment.min.js",
				"~/Scripts/angular-ui-tree-v2.15.0/angular-ui-tree.min.js",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt.js",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt-tree-plugin.js",
				//"~/Scripts/angular-gantt-1.2.6/angular-gantt-progress-plugin.js",
				"~/Scripts/ng-map.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/app")
				.IncludeDirectory("~/app", "*.js", false)
				.IncludeDirectory("~/app/Config", "*.js", true)
				.IncludeDirectory("~/app/BaseControllers", "*.js", true)
				.IncludeDirectory("~/app/Directives", "*.js", true)
				.IncludeDirectory("~/app/Filters", "*.js", true)
				.IncludeDirectory("~/app/Services", "*.js", true)
				.IncludeDirectory("~/app/Controllers", "*.js", true));
		}
	}
}