using System.Web.Mvc;
using System.Web.Routing;
using travel_web.Common;
using travel_web.init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel_web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App.Common = new WebCommon();
        }
    }
}
