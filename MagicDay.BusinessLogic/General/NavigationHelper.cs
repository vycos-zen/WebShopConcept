using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDay.BusinessLogic.General
{
    public sealed class NavigationHelper
    {
        private NavigationHelper()
        {
        }
        public static string ErrorPage(ErrorType errorType)
        {
            return "~/Pages/Error/ErrorPage.aspx?type=" + errorType.ToString();
        }

        public static string CategoriesView()
        {
            return "~/Pages/Categories/CategoriesView.aspx";
        }

        public static string CategoriesEditNew()
        {
            return "~/Pages/Categories/CategoriesEdit.aspx?action=new";
        }
        public static string CategoriesEdit(string categoryID)
        {
            return "~/Pages/Categories/CategoriesEdit.aspx?action=edit&guid=" + categoryID;
        }
        public static string ProductsView()
        {
            return "~/Pages/Products/ProductsView.aspx";
        }

        public static string ProductsEditNew()
        {
            return "~/Pages/Products/ProductsEdit.aspx?action=new";
        }
        public static string ProductsEdit(string productID)
        {
            return "~/Pages/Products/ProductsEdit.aspx?action=edit&guid=" + productID;
        }

        public static string ProductImageManager(string productID)
        {
            return "~/Pages/ImageService/ProductImageManager.aspx?guid="+productID;
        }

        public static string DisplayProductThumbnail(string productImageID, string imageNo)
        {
            return "~/Pages/ImageService/DisplayImage.ashx?source=productImage&type=thumbnail&guid=" + productImageID + "&imageNumber=" + imageNo;
        }

        public static string DisplayProductImage(string productImageID, string imageNo)
        {
            return "~/Pages/ImageService/DisplayImage.ashx?source=productImage&type=fullSizeImage&guid=" + productImageID + "&imageNumber=" + imageNo;
        }

        public static string DisplayTempProductImageThumbnail(string productImageID)
        {
            return "~/Pages/ImageService/DisplayImage.ashx?source=tempProductImage&type=thumbnail&guid=" + productImageID;
        }

        public static string DisplayTempProductImage(string productImageID)
        {
            return "~/Pages/ImageService/DisplayImage.ashx?source=tempProductImage&type=fullSizeImage&guid=" + productImageID;
        }
    }
}
