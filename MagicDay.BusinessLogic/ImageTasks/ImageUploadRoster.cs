using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MagicDay.BusinessLogic.General;

namespace MagicDay.BusinessLogic.ImageTasks
{
    [DataObject()]
    public class ImageUploadRoster
    {
        //    public int NumberOfImages {
        //        get
        //        {
        //            if (ImageRoster != null)
        //            {
        //                return ImageRoster.Count();
        //                    }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //    }
        //    public List<ImageUploadItem> ImageRoster { get; set; }
        //    public UploadStatus UploadStatus;

        //    public ImageUploadRoster()
        //    {
        //        ImageRoster = new List<ImageUploadItem>();
        //        UploadStatus = new UploadStatus();
        //    }

        //    [DataObjectMethod(DataObjectMethodType.Select, true)]
        //    public List<ImageUploadItem> GetAllImages()
        //    {
        //        return ImageRoster;
        //    }

        //    [DataObjectMethod(DataObjectMethodType.Delete, true)]
        //    public void RemoveImageFromUploadRoster(Guid id)
        //    {
        //        if (ImageRoster != null && ImageRoster.Count > 0)
        //        {
        //            ImageUploadItem image = (from img in ImageRoster
        //                                     where img.ImageID == id
        //                                     select img).First();
        //            ImageRoster.Remove(image);                
        //        }
        //    }



        }

    }


