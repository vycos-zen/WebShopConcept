using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MagicDay.Admin
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        private string errorType;
        private string errorTitleDisplay = "Oops... There was an error. We appologise for the inconvenience.";
        protected void Page_Load(object sender, EventArgs e)
        {
            errorType = Request.QueryString["type"];
            lblErrorTitle.Text = errorTitleDisplay;
            switch (errorType)
            {
                case "FourOFour":
                    lblError.Text = "The requested page was not found. (404)";
                    break;

                case "General":
                    lblError.Text = "The page had trouble processing the request.";
                    break;
                case "Unandled":
                    lblError.Text = "The page has run into an unhandled error.";
                    break;
                default:
                    lblError.Text = "Unknown error.";
                    break;
                case "Unauthorized":
                    lblError.Text = "Looks like you are not authorized to view the requested page.";
                    break;
            }
        }
    }
}