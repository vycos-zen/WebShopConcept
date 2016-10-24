using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MagicDay.DataModel;
using System.Web.ModelBinding;
using MagicDay.BusinessLogic.DataAccess;
using System.IO;
using MagicDay.BusinessLogic.ImageTasks;
using MagicDay.BusinessLogic.General;

namespace MagicDay.Admin
{
    public partial class ProductImageManager : System.Web.UI.Page
    {
        private ProductImageData productImageData = new ProductImageData();
        private Conversion convertion = new Conversion();
        public string DisplayProductThumbnail(string productImageID, string imageNo)
        {
            return NavigationHelper.DisplayProductThumbnail(productImageID, imageNo);
        }
        public string DisplayTempProductImageThumbnail(string productImageID)
        {
            return NavigationHelper.DisplayTempProductImageThumbnail(productImageID);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.Enctype = "multipart/form-data";
            if (!IsPostBack)
            {
                Session.Add("tempProductImages", new List<ImageUploadItem>());
            }
            else
            {
                if (null == Session["tempProductImages"])
                {
                    ModelState.AddModelError("", "Unable to locate upload list.");
                }
                else
                {
                    lstTempImages.DataSource = (List<ImageUploadItem>)Session["tempProductImages"];
                    lstTempImages.DataBind();
                }
            }

            if (null != Context.Request.QueryString["guid"])
            {
                Guid productID;
                bool isProductIDValid = Guid.TryParse(Context.Request.QueryString["guid"], out productID);
                if (isProductIDValid)
                {
                    if(productImageData.GetProductName(productID) == null)
                    {
                        ModelState.AddModelError("", "Unable to locate product.");
                    }
                    else
                    {
                    ViewState.Add("ProductID", productID);
                    lblProductName.DataBind();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid prduct ID.");
                }
            }
        }

        public IList<ProductImage> lstProductImages_GetData([QueryString]Guid? guid)
        {
            if (null != guid)
            {
                return productImageData.GetProductImages(guid.Value);
            }
            return null;
        }

        protected void btnAddTempImages_Click(object sender, EventArgs e)
        {
            if (null == Session["tempProductImages"])
            {
                ModelState.AddModelError("", "Unable to locate upload list.");
                return;
            }
            if (fplImageUpload.PostedFiles.First().ContentLength > 0)
            {
                for (int i = 0; i < fplImageUpload.PostedFiles.Count; i++)
                {
                    using (var binaryReader = new BinaryReader(fplImageUpload.PostedFiles[i].InputStream))
                    {
                        byte[] imageData = null;

                        imageData = binaryReader.ReadBytes(fplImageUpload.PostedFiles[i].ContentLength);
                        ImageUploadItem imageItem = new ImageUploadItem();
                        imageItem.ImageID = Guid.NewGuid();
                        imageItem.ImageThumbnail = convertion.ReSizeOriginalImage(imageData, 120, 30L);
                        imageItem.Image = convertion.ReSizeOriginalImage(imageData, 800, 80L);
                        imageItem.ImageMimeType = "image/jpg";

                        if (null != (Guid)ViewState["ProductID"])
                        {
                            imageItem.ProductID = (Guid)ViewState["ProductID"];
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error on page, product not selected.");
                        }
                            ((List<ImageUploadItem>)Session["tempProductImages"]).Add(imageItem);
                    }
                }
                lstTempImages.DataSource = (List<ImageUploadItem>)Session["tempProductImages"];
                lstTempImages.DataBind();
            }
        }

        protected void lstTempImages_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lstTempImages.EditIndex = e.NewEditIndex;
            lstTempImages.DataBind();
        }

        protected void lstTempImages_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            if (null == Session["tempProductImages"])
            {
                ModelState.AddModelError("", "Unable to locate upload list.");
                return;
            }
            ImageUploadItem tempImage = ((IList<ImageUploadItem>)Session["tempProductImages"]).Where(i => i.ImageID == (Guid)e.Keys[0]).FirstOrDefault();
            tempImage.ImageDescription = e.NewValues["ImageDescription"].ToString();
            lstTempImages.EditIndex = -1;
            lstTempImages.DataSource = (List<ImageUploadItem>)Session["tempProductImages"];
            lstTempImages.DataBind();
        }

        protected void lstTempImages_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lstTempImages.EditIndex = -1;
            lstTempImages.DataBind();
        }

        protected void lstTempImages_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            if (null == Session["tempProductImages"])
            {
                ModelState.AddModelError("", "Unable to locate upload list.");
                return;
            }
            ImageUploadItem tempImage = ((IList<ImageUploadItem>)Session["tempProductImages"]).Where(i => i.ImageID == (Guid)e.Keys[0]).FirstOrDefault();
            var tempImageList = ((List<ImageUploadItem>)Session["tempProductImages"]);
            tempImageList.Remove(tempImage);
            lstTempImages.DataSource = (List<ImageUploadItem>)Session["tempProductImages"];
            lstTempImages.DataBind();
            Page.Validate();
        }

        protected void btnSubmitUpload_Click(object sender, EventArgs e)
        {
            if (null == Session["tempProductImages"])
            {
                ModelState.AddModelError("", "Unable to locate upload list.");
                return;
            }
            if (ModelState.IsValid)
            {
                var tempImageList = ((List<ImageUploadItem>)Session["tempProductImages"]).ToList();
                productImageData.InsertProductImages(tempImageList);
                ((List<ImageUploadItem>)Session["tempProductImages"]).Clear();
                lstTempImages.DataSource = ((List<ImageUploadItem>)Session["tempProductImages"]);
                lstTempImages.DataBind();
                Response.Redirect(Request.RawUrl);
            }

        }

        protected void btnCancelUpload_Click(object sender, EventArgs e)
        {
            if (null == Session["tempProductImages"])
            {
                Response.Redirect(Request.RawUrl);
            }
            ((List<ImageUploadItem>)Session["tempProductImages"]).Clear();
            lstTempImages.DataSource = (List<ImageUploadItem>)Session["tempProductImages"];
            lstTempImages.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        public void lstProductImages_UpdateItem()
        {
            ProductImage productImage = new ProductImage();
            TryUpdateModel(productImage);
            if (ModelState.IsValid)
            {
                productImageData.UpdateProductImage(productImage);
                Response.Redirect(Request.RawUrl);
            }
        }

        public void lstProductImages_DeleteItem()
        {
            var deleteProductImageID = (Guid)lstProductImages.SelectedDataKey.Value;
            productImageData.RemoveProductImage(deleteProductImageID);
            lstTempImages.EditIndex = -1;
            Page.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void lstProductImages_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lstTempImages.EditIndex = -1;
            lstTempImages.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void lstProductImages_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            lstProductImages.SelectedIndex = e.ItemIndex;
        }

        protected void lstProductImages_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            lstProductImages.SelectedIndex = e.ItemIndex;
        }

        protected void cvCheckMaxNumberOfImage_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (null == Session["tempProductImages"])
            {
                args.IsValid = true;
                return;
            }

            if (null == (Guid)ViewState["ProductID"])
            {
                args.IsValid = false;
                ModelState.AddModelError("", "No product has been selected.");
                return;
            }

            Guid productID;
            bool isProductIDValid = Guid.TryParse(ViewState["ProductID"].ToString(), out productID);
            if (isProductIDValid)
            {
                int currentNumberOfUploadImages = ((List<ImageUploadItem>)Session["tempProductImages"]).ToList().Count();
                int currentNumberOfProductImages = productImageData.GetNumberOfProductImages(productID);
                int currentNumberOfFileUploadImages = 0;
                if (fplImageUpload.HasFiles && fplImageUpload.PostedFiles[0].ContentLength > 0)
                {
                    currentNumberOfFileUploadImages = fplImageUpload.PostedFiles.Count();
                }
                if ((currentNumberOfProductImages +
                    currentNumberOfUploadImages +
                   currentNumberOfFileUploadImages) <= 10)
                {
                    ModelState.Remove("errNumberOfImages");
                    args.IsValid = true;
                    return;
                }
                else
                {
                    ModelState.AddModelError("errNumberOfImages", "Maximum allowed images per product is 10. Please remove the access images");
                    return;
                }
            }
            else
            {
                args.IsValid = false;
                ModelState.AddModelError("", "Invalid product id.");
                return;
            }
        }

        public string GetProductNameOfImages()
        {
            return productImageData.GetProductName((Guid)ViewState["ProductID"]);
        }
    }
}
