using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AdventureWorksWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var cssDesktopBundle = new Bundle("~/Content/css", new CssMinify());            
            cssDesktopBundle.AddDirectory("~/Content/desktop", "*.css", true);
            cssDesktopBundle.AddDirectory("~/Content", "Site.css", false);
            BundleTable.Bundles.Add(cssDesktopBundle);

            var cssMobileBundle = new Bundle("~/Content/Mobile/css", new CssMinify());            
            cssMobileBundle.AddDirectory("~/Content/mobile", "*.css", true);
            cssMobileBundle.AddDirectory("~/Content", "Site.Mobile.css", false);
            BundleTable.Bundles.Add(cssMobileBundle);

            var jsDesktopBundle = new Bundle("~/Scripts/js");
            jsDesktopBundle.AddDirectory("~/Scripts/Lib", "*.js", true);
            jsDesktopBundle.AddDirectory("~/Scripts", "*.js", false);
            BundleTable.Bundles.Add(jsDesktopBundle);

            var jsMobileBundle = new Bundle("~/Scripts/Mobile/js");
            jsMobileBundle.AddDirectory("~/Scripts/Lib", "*.js", true);
            jsMobileBundle.AddDirectory("~/Scripts/MobileLib", "*.js", true);
            jsMobileBundle.AddDirectory("~/Scripts", "*.js", false);
            BundleTable.Bundles.Add(jsMobileBundle);

            DependencyResolver.SetResolver(new AdventureWorksDependencyResolver(AdventureWorksUnityContainer.Instance));
        }
    }
}