using MagicDay.BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDay.BusinessLogic.ImageTasks
{
   public class Display
    {
        public string DisplayProductImage(int imageNo, Guid? productId)
        {
            using (MagicDayEntities dataModel = new MagicDayEntities())
            {
                if (productId == null)
                {
                    return "";
                }
                else
                {
                    ProductImage image = (from img in dataModel.ProductImages
                                            where img.ProductID == productId && img.ImageNo == imageNo
                                            select img).FirstOrDefault();

                    if (image != null)
                    {
                        return "data:image/jpg;base64," + Convert.ToBase64String((byte[])image.ImageThumbnail);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        public string ImageServerUrl()
        {
            return "~/DisplayImage.ashx";
            //return "http://localhost:51356/DisplayImage.ashx";
        }

        public string DisplayImagePreUpload(byte[] image)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String(image);
        }

    }
}
