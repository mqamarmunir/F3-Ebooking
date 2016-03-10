using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation.Providers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BusinessRules;
namespace EBooking
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            //StaticCache.LoadStaticCache();
            System.Timers.Timer timer=new System.Timers.Timer(120000);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            GlobalConfiguration.Configuration.Services.RemoveAll( typeof(System.Web.Http.Validation.ModelValidatorProvider), v => v is InvalidModelValidatorProvider);
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //System.Web.HttpContext.Current.Cache.Remove("key");
            //StaticCache.LoadStaticCache(System.DateTime.Now.ToString("HHmmss"));
            StaticCache objcache = new StaticCache();
            objcache.GetDropDowns(true);

        }
        public void Session_OnStart()
        {
          
        }

    }
}