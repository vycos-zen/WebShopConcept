using MagicDay.BusinessLogic.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace MagicDay.Admin
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.1.1.min.js",
                DebugPath = "~/Scripts/jquery-3.1.1.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.1.1.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.1.1.js",
            });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            var exType = ex.GetType();

            switch(exType.Name)
            {
                case "UnauthorizedAccessException":
                    Response.Redirect(NavigationHelper.ErrorPage(ErrorType.Unauthorized));
                    break;
                case "HttpUnhandledException":
                    ErrorLogger.LogException(ex, "Global");
                    Response.Redirect(NavigationHelper.ErrorPage(ErrorType.Unhandled));
                    break;
                default:
                    Response.Redirect(NavigationHelper.ErrorPage(ErrorType.General));
                    break;
            }
        }
    }
}