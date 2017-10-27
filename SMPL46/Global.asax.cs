using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SMPL46.Services.Logging.Elmah;
using NLog;

namespace SMPL46
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Set up NLog file source for logging
            string nlogPath = Server.MapPath("nlog-web.log");
            NLog.Common.InternalLogger.LogFile = nlogPath;
            NLog.Common.InternalLogger.LogLevel = NLog.LogLevel.Trace;

            // Add Mobile Bundles
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
