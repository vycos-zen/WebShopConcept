using MagicDay.BusinessLogic.ImageTasks;
using MagicDay.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDay.BusinessLogic.DataAccess
{
    public class ProductImageData
    {
        public IList<ProductImage> GetProductImages(Guid guid)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var productImages = (from productImage in dataModel.ProductImages
                                     where productImage.ProductID == guid
                                     select productImage)
                                     .OrderBy(p => p.ImageNo)
                                     .ToList();
                return productImages;
            }
        }

        public void InsertProductImages(List<ImageUploadItem> tempProductImage)
        {
            using (var dataModel = new MagicDayEntities())
            {
                if (null == tempProductImage || tempProductImage.Count <= 0)
                {
                    return;
                }
                Guid productID = tempProductImage[0].ProductID;
                Product product = dataModel.Products.Where(p => p.ProductID == productID).FirstOrDefault();
                int productImagesCount = product.ProductImages.Count();
                for (int i = 0; i < tempProductImage.Count; i++)
                {
                    ProductImage productImage = new ProductImage();
                    productImage.ProductImageID = tempProductImage[i].ImageID;
                    productImage.ImageDesciption = tempProductImage[i].ImageDescription;
                    productImage.ImageMimeType = tempProductImage[i].ImageMimeType;
                    productImage.ImageNo = Convert.ToByte(productImagesCount + 1);
                    productImagesCount++;
                    productImage.ImageThumbnail = tempProductImage[i].ImageThumbnail;
                    productImage.Image = tempProductImage[i].Image;
                    productImage.ProductID = tempProductImage[i].ProductID;
                    dataModel.ProductImages.Add(productImage);
                }
                dataModel.SaveChanges();
            }
        }

        public int GetNumberOfProductImages(Guid productID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                int numberOfProductImages = dataModel.Products.First(p => p.ProductID == productID).ProductImages.Count();
                return numberOfProductImages;
            }
        }

        public void RemoveProductImage(Guid deleteProductImageID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var productImageToRemove = dataModel.ProductImages.Find(deleteProductImageID);
                if (null == productImageToRemove)
                {
                    return;
                }
                var productID = productImageToRemove.ProductID;
                dataModel.ProductImages.Remove(productImageToRemove);
                var productImageList = dataModel.ProductImages.Where(p => p.ProductID == productID);
                int imageNo = 1;
                foreach (var image in productImageList)
                {
                    if(dataModel.Entry(image).State != System.Data.Entity.EntityState.Deleted)
                    {
                    image.ImageNo = Convert.ToByte(imageNo);
                    imageNo++;
                    }
                }
                dataModel.SaveChanges();
            }
        }

        public void UpdateProductImage(ProductImage productImage)
        {
            using (var dataModel = new MagicDayEntities())
            {
                ProductImage productToUpdate = dataModel.ProductImages.Find(productImage.ProductImageID);
                productToUpdate.ImageDesciption = productImage.ImageDesciption;
                dataModel.SaveChanges();
            }
        }

        public string GetProductName(Guid productID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var product = dataModel.Products.Find(productID);
                if (null == product)
                {
                    return null;
                }
                return product.ProductName;
            }
        }

        public ProductImage GetProductImage(Guid productImageID, int imageNo)
        {
            using (var dataModel = new MagicDayEntities())
            {
                ProductImage productImage = dataModel.ProductImages.Where(i => i.ProductImageID == productImageID && i.ImageNo == imageNo).FirstOrDefault();
                if(null == productImage)
                {
                    return null;
                }
                return productImage;
            }

        }

    }
}
