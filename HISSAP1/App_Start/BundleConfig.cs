using System.Web;
using System.Web.Optimization;

namespace HISSAP1
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

      bundles.Add(new ScriptBundle("~/bundles/site").Include(
          "~/Scripts/js/header.js",
          "~/Scripts/js/budgetInvoice.js"
          ));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                /* Base code that is general and present on potentially every page */
                "~/Content/base/normalize.css",
                "~/Content/base/global.css",
                "~/Content/base/list.css",
                "~/Content/base/text.css",
                "~/Content/base/table.css",
                "~/Content/base/form.css",
                "~/Content/base/btn.css",
                "~/Content/base/section.css",
                /*Large chunks of code dedicated to a single aspect of a webpage*/
                "~/Content/components/alert.css",
                "~/Content/components/breadcrumb.css",
                "~/Content/components/grp-button.css",
                "~/Content/components/grp-input.css",
                "~/Content/components/grp-list.css",
                "~/Content/components/header.css",
                "~/Content/components/label.css",
                "~/Content/components/modal.css",
                "~/Content/components/pager.css",
                "~/Content/components/pagination.css",
                "~/Content/components/panel.css",
                "~/Content/components/progress-bar.css",
                "~/Content/components/thumbnail.css",
                "~/Content/components/tooltip.css",
                "~/Content/components/well.css",
                "~/Content/components/image-button.css",
                /*Localized code tied to locations on a webpage*/
                "~/Content/modules/header.css",
                "~/Content/modules/login.css",
                "~/Content/modules/budget.css",
                "~/Content/modules/assess-and-plan.css"
                ));
    }
  }
}
