using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MagicDay.BusinessLogic.DataAccess;
using MagicDay

namespace MagicDay.Images
{
    public sealed class DisplayImageAsync : IAsyncResult
    {
        private bool completed;
        private Object state;
        private AsyncCallback callback;
        private HttpContext context;
        private byte[] imageToReturn;

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
            using (var dataSource = new MagicDayEntities())
            {
                string source = context.Request.QueryString["source"];
                string type = context.Request.QueryString["type"];
                string guid = context.Request.QueryString["guid"];
                string imageNumberQueryString = context.Request.QueryString["imageNumber"];
                int imageNumber = (!String.IsNullOrEmpty(imageNumberQueryString) ? Int32.Parse(imageNumberQueryString) : 0);

                if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(type) || String.IsNullOrEmpty(guid))
                {
                    ReturnEmptyImage();
                }
                else
                {
                    Guid imageGuid;
                    bool isGuid = Guid.TryParse(guid, out imageGuid);
                    switch (source)
                    {
                        case "gallery":
                            //var galleryImages = dataSource.GalleryImageEDs;
                            //Guid imageGuid;
                            //bool isGuid = Guid.TryParse(guid, out imageGuid);
                            //if (isGuid)
                            //{
                            //    GalleryImageED image = (from img in galleryImages
                            //                            where img.GalleryImageID == imageGuid
                            //                            select img).FirstOrDefault();
                            //    if (type == "thumbnail")
                            //    {
                            //        imageToReturn = image.GalleryImageThumbnail;
                            //    }
                            //    else if (type == "fullSizeImage")
                            //    {
                            //        imageToReturn = image.GalleryImage;
                            //    }
                            //    else
                            //    {
                            //        ReturnEmptyImage();
                            //    }
                            //}
                            //else
                            //{
                            //    ReturnEmptyImage();
                            //}
                            break;

                        case "product":
                            var productImages = dataSource.ProductImages;
                            isGuid = Guid.TryParse(guid, out imageGuid);
                            if (isGuid)
                            {
                                ProductImage image = (from img in productImages
                                                        where img.ProductImageID == imageGuid && img.ImageNo == imageNumber
                                                        select img).FirstOrDefault();
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
                                }
                            }
                            else
                            {
                                ReturnEmptyImage();
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
        }

        private void ReturnEmptyImage()
        {
            byte[] emptyImage = new byte[0];
            context.Response.ContentType = "image/jpg";
            context.Response.OutputStream.Write(emptyImage, 0, 0);
        }
    }
}
