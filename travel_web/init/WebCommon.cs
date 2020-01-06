using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using travel.Entities;
using travel_web.Common;

namespace travel_web.init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            if(HttpContext.Current.Session["login"] != null)
            {
                travelUser user = HttpContext.Current.Session["login"] as travelUser;
                return user.Username;
            }
            return "system";
        }
    }
}