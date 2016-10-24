using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.SessionState;

namespace MagicDay.Admin
{
    /// <summary>
    /// This handler hosting image display for MagicDay pages
    /// /// </summary>
    public class DisplayImage : IHttpAsyncHandler, IReadOnlySessionState
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
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }

    }
}