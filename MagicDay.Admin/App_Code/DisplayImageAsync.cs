using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MagicDay.BusinessLogic.DataAccess;
using MagicDay.DataModel;
using MagicDay.BusinessLogic.ImageTasks;

namespace MagicDay.Admin
{
    public sealed class DisplayImageAsync : IAsyncResult
    {
        private bool completed;
        private Object state;
        private AsyncCallback callback;
        private HttpContext context;
        private byte[] imageToReturn;
        private ProductImageData productImageData = new ProductImageData();

        bool IAsyncResult.IsCompleted { get { return completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public DisplayImageAsync(AsyncCallback callback, HttpContext context, object state)
        {
            this.callback = callback;
            this.context = context;
            this.state = state;
            this.completed = false;
        }

        public void StartAsyncDisplay()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(object workItemState)
        {
            string source = context.Request.QueryString["source"];
            string type = context.Request.QueryString["type"];
            string guid = context.Request.QueryString["guid"];
            string imageNumberQueryString = context.Request.QueryString["imageNumber"];
            int imageNumber = (!String.IsNullOrEmpty(imageNumberQueryString) ? Int32.Parse(imageNumberQueryString) : 0);

            if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(type) || String.IsNullOrEmpty(guid))
            {
                ReturnEmptyImage();
                return;
            }
            else
            {
                Guid imageGuid;
                bool isGuid = Guid.TryParse(guid, out imageGuid);
                switch (source)
                {
                    case "tempProductImage":
                        Guid tempProductImageID;
                        bool isImageIDValid = Guid.TryParse(guid, out tempProductImageID);
                        if (isImageIDValid && null != context.Session["tempProductImages"])
                        {
                            ImageUploadItem tempProductImage = ((List<ImageUploadItem>)context.Session["tempProductImages"]).Where(i => i.ImageID == tempProductImageID).FirstOrDefault();
                            if (type == "thumbnail")
                            {
                                imageToReturn = tempProductImage.ImageThumbnail;
                            }
                            else if (type == "fullSizeImage")
                            {
                                imageToReturn = tempProductImage.Image;
                            }
                            else
                            {
                                ReturnEmptyImage();
                            }
                        }
                        else
                        {
                            ReturnEmptyImage();
                        }
                        break;

                    case "productImage":
                        if (imageNumber <= 0)
                        {
                            ReturnEmptyImage();
                            return;
                        }
                        isGuid = Guid.TryParse(guid, out imageGuid);
                        if (isGuid)
                        {

                            ProductImage image = productImageData.GetProductImage(imageGuid, imageNumber);
                            if (null == image)
                            {
                                ReturnEmptyImage();
                                return;
                            }
                            if (type == "thumbnail")
                            {
                                imageToReturn = image.ImageThumbnail;

                            }
                            else if (type == "fullSizeImage")
                            {
                                imageToReturn = image.Image;
                            }
                            else
                            {
                                ReturnEmptyImage();
                                return;
                            }
                        }
                        else
                        {
                            ReturnEmptyImage();
                            return;
                        }
                        break;
                    default:
                        break;
                }
                context.Response.ContentType = "image/jpg";
                context.Response.OutputStream.Write(imageToReturn, 0, imageToReturn.Length);
                completed = true;
                callback(this);
            }
        }

        private void ReturnEmptyImage()
        {
            byte[] emptyImage = new byte[0];
            context.Response.ContentType = "image/jpg";
            context.Response.OutputStream.Write(emptyImage, 0, 0);
        }
    }
}
