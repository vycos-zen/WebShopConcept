using MagicDay.BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDay.BusinessLogic.ImageTasks
{
    //used as temporary data holder when products are being uploaded

    [DataObject()]
    public class TempProductImages
    {
        public TempProductImages()
        {
            Images = new Dictionary<int, ProductImage>();
        }
        public Dictionary<int, ProductImage> Images { get; set; }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Dictionary<int, ProductImage> GetUploadImages()
        {
            return Images;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveImage(Guid id)
        {
            Images.Remove((from img in Images
                           where img.Value.ProductImageID == id
                           select img.Key).FirstOrDefault());
            SetImageNumbers();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddImage(ProductImage image)
        {
            int imageNumber = Images.Count + 1;
            Images.Add(imageNumber, image);
            var images = (from img in Images
                          orderby img.Key ascending
                          select img);
            Images = images.ToDictionary<KeyValuePair<int,ProductImage>, int, ProductImage>(img => img.Key, img => img.Value);
        }

        public void SetImageNumbers()
        {
            int nTh = 1;
            int imageNumber;
            ProductImage productImage;
            foreach (var image in Images.ToList())
            {
                imageNumber = image.Key;
                productImage = image.Value;
                Images.Remove(image.Key);
                Images.Add(nTh, productImage);
                Images[nTh].ImageNo = Convert.ToByte(nTh);
                ++nTh;
            }

        }
    }
}
