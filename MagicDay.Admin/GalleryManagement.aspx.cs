using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MagicDay.ClassLibrary.DataModel;
using MagicDay.ClassLibrary.Classes;
using System.IO;
using System.Drawing.Imaging;

namespace MagicDay.Admin
{
    public partial class GalleryManagement : System.Web.UI.Page
    {
        private ImageUploadRoster uploadRoster;
        protected MagicDayCommonOperations mdOperations = new MagicDayCommonOperations();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Enctype = "multipart/form-data";

            if (Session["ImageRoaster"] == null)
            {
                uploadRoster = new ImageUploadRoster();
                Session["ImageRoaster"] = uploadRoster as ImageUploadRoster;
            }
            else
            {
                uploadRoster = Session["ImageRoaster"] as ImageUploadRoster;
            }
            btnUploadImage.Visible = (imageUpload.HasFiles || uploadRoster.NumberOfImages > 0);
            setNamesPrompt.Visible = (uploadRoster.NumberOfImages > 0);
            uploadLimitLabel.DataBind();
        }
        protected void btnUploadImages_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    if (this.imageUpload.PostedFiles.Count > 0 && Page.IsValid)
                    {
                        for (int i = 0; i < imageUpload.PostedFiles.Count; i++)
                        {
                            byte[] imageData = null;
                            ImageUploadItem imageItem = new ImageUploadItem();

                            switch (imageUpload.PostedFiles[i].ContentType)
                            {
                                case "image/jpeg":
                                    {
                                        using (var binaryReader = new BinaryReader(imageUpload.PostedFiles[i].InputStream))
                                        {
                                            imageData = binaryReader.ReadBytes(imageUpload.PostedFiles[i].ContentLength);
                                        }
                                        break;
                                    }
                                case "image/png":
                                    {
                                        using (System.Drawing.Image pngImage = System.Drawing.Image.FromStream(imageUpload.PostedFiles[i].InputStream))
                                        {
                                            MemoryStream streamToJpg = new MemoryStream();
                                            pngImage.Save(streamToJpg, ImageFormat.Jpeg);
                                            imageData = streamToJpg.ToArray();
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        imageItem.ImageId = Guid.NewGuid();
                                        imageItem.ImageName = imageUpload.PostedFiles[i].FileName.Split('\\').Last();
                                        imageItem.ImageName = imageItem.ImageName.Split('.').First();
                                        imageItem.ImageThumbnail = mdOperations.ReSizeOriginalImage(imageData, 120, 30L);
                                        imageItem.Image = mdOperations.ReSizeOriginalImage(imageData, 800, 80L);
                                        imageItem.ImageMimeType = "image/jpg";
                                        uploadRoster.ImageRoster.Add(imageItem);
                                        Session["UploadStatus"] += imageUpload.PostedFiles[i].FileName + " is not an image.<br/>";
                                        break;
                                    }
                            }
                        }
                        uploadRoster.UploadStatus = UploadStatus.PendingUpload;
                        Session["ImageRoaster"] = uploadRoster as ImageUploadRoster;
                        setNamesPrompt.Visible = (uploadRoster.NumberOfImages > 0);
                        imageUploadRoster.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    //for debug only
                    // uploadStatus.Text = ex.Message;
                }
            }
        }
        protected void ImageUploadRosterDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            if (Session["ImageRoaster"] != null)
            {
                if (uploadRoster.ImageRoster.Count > 0 && uploadRoster.UploadStatus == UploadStatus.PendingUpload)
                {
                    e.ObjectInstance = Session["ImageRoaster"] as ImageUploadRoster;
                }
                else if (uploadRoster.UploadStatus == UploadStatus.NewUpload)
                {
                    return;
                }
            }
        }
        public void uploadImage_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && Session["ImageRoaster"] != null)
            {
                uploadRoster = Session["ImageRoaster"] as ImageUploadRoster;
                if (uploadRoster.ImageRoster.Count > 0)
                {
                    SaveImagesToDB(uploadRoster);
                }
                ClearImageRoaster();
            }
        }
        private void SaveImagesToDB(ImageUploadRoster uploadRoster)
        {
            try
            {
                using (MagicDayDBEntities dataModel = new MagicDayDBEntities())
                {
                    foreach (ImageUploadItem image in uploadRoster.ImageRoster)
                    {
                        GalleryImageED galleryImage = new GalleryImageED();
                        galleryImage.GalleryImageID = image.ImageId;
                        galleryImage.GalleryImageUploadDate = DateTime.Now;
                        galleryImage.GalleryImageName = image.ImageName;
                        galleryImage.GalleryImage = image.Image;
                        galleryImage.GalleryImageThumbnail = image.ImageThumbnail;
                        galleryImage.GalleryImageMimeType = image.ImageMimeType;
                        dataModel.GalleryImageEDs.Add(galleryImage);
                    }
                    dataModel.SaveChanges();
                    toggleNewUploadRoster();
                }
            }
            catch (Exception ex)
            {
                //for debug only
                string exc = ex.Message;
            }
        }
        protected void ClearImageRoaster()
        {
            if (uploadRoster != null)
            {
                Session["ImageRoaster"] = null;
                Page.DataBind();
            }
        }
        protected void imageUploadValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["ImageRoaster"] != null)
            {
                int numberOfImagesInFileUpload = (Session["ImageRoaster"] as ImageUploadRoster).NumberOfImages;
                numberOfImagesInFileUpload += imageUpload.PostedFiles.Count;
                args.IsValid = (numberOfImagesInFileUpload <= 25);
            }
        }
        protected void imageUploadRoster_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {
                Guid id;
                Guid.TryParse(e.CommandArgument.ToString(), out id);
                uploadRoster.RemoveImageFromUploadRoster(id);
                Session["ImageRoaster"] = uploadRoster as ImageUploadRoster;
                if (uploadRoster.NumberOfImages == 0)
                {
                    toggleNewUploadRoster();
                }
                uploadLimitLabel.DataBind();
                imageUploadRoster.DataBind();
            }
        }
        protected void imageUploadRoster_EditCommand(object source, DataListCommandEventArgs e)
        {
            imageUploadRoster.EditItemIndex = e.Item.ItemIndex;
            imageUploadRoster.DataBind();
        }
        protected void imageUploadRoster_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            {
                var image = uploadRoster.ImageRoster.FirstOrDefault(img => img.ImageId == Guid.Parse(e.CommandArgument.ToString()));
                image.ImageName = ((TextBox)e.Item.FindControl("ImageName")).Text;
                imageUploadRoster.EditItemIndex = -1;
                imageUploadRoster.DataBind();
            }
        }
        public string NumberOfImagesInRoaster()
        {
            string numberOfImg;
            if ((uploadRoster != null && uploadRoster.NumberOfImages > 0) || imageUpload.HasFiles)
            {
                numberOfImg = "(" + (uploadRoster.NumberOfImages + imageUpload.PostedFiles.Count).ToString() + "/25)";
                return numberOfImg;
            }
            return "";
        }
        protected void imageList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                Guid galleryImageId = Guid.Parse(e.CommandArgument.ToString());
                using (MagicDayDBEntities dataModel = new MagicDayDBEntities())
                {
                    GalleryImageED galleryImage = (from image in dataModel.GalleryImageEDs
                                                   where image.GalleryImageID == galleryImageId
                                                   select image).FirstOrDefault();
                    galleryImage.GalleryImageName = ((TextBox)e.Item.FindControl("GalleryImageName")).Text;
                    dataModel.SaveChanges();
                    imageList.DataBind();
                }
            }
        }
        protected void btnNewUploadRosterToggle_Click(object sender, EventArgs e)
        {
            toggleNewUploadRoster();
        }
        private void toggleNewUploadRoster()
        {
            imageUploadPanel.Visible = !imageUploadPanel.Visible;
            uploadStatus.Text = "";
            uploadLimitLabel.Text = "";
            if (uploadRoster != null)
            {
                uploadRoster.UploadStatus = (uploadRoster.UploadStatus == UploadStatus.PendingUpload) ? UploadStatus.NewUpload : UploadStatus.PendingUpload;
                btnNewUploadRosterToggle.Text = (btnNewUploadRosterToggle.Text == "Cancel") ? "Cancel" : "New Upload";
                if (!imageUploadPanel.Visible)
                {
                    ClearImageRoaster();
                }
            }
        }
    }
}
