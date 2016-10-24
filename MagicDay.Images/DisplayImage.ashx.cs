using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Threading;

namespace MagicDay.Images
{
    /// <summary>
    /// This handler will be hosted as a subdomain of magicday, and it will serv as the image display service for the domain
    /// </summary>
    public class DisplayImage : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            DisplayImageAsync DisplayImageAsync = new DisplayImageAsync(cb, context, extraData);
            DisplayImageAsync.StartAsyncDisplay();
            return DisplayImageAsync;
        }
        public void EndProcessRequest(IAsyncResult result)
        {
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }

    }
}