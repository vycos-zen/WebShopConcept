using MagicDay.BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MagicDay.DataModel;
using System.Web.ModelBinding;
using MagicDay.BusinessLogic.General;

namespace MagicDay.Admin
{
    public partial class ProductsEdit : System.Web.UI.Page
    {

        private ProductData productData = new ProductData();
        private CategoryData categoryData = new CategoryData();
        private string requestType;
        protected void Page_Load(object sender, EventArgs e)
        {
            requestType = Context.Request.QueryString["action"];
            switch (requestType)
            {
                case "new":
                    frmvProductEdit.ChangeMode(FormViewMode.Insert);
                    break;
                case "edit":
                    frmvProductEdit.ChangeMode(FormViewMode.Edit);
                    break;
                default:
                    break;
            }
        }

        public Product frmvProductEdit_GetItem([QueryString]Guid? guid)
        {
            var product = new Product();
            if (frmvProductEdit.CurrentMode == FormViewMode.Insert)
            {
                return product;
            }
            else if (frmvProductEdit.CurrentMode == FormViewMode.Edit && null != guid)
            {
                product = productData.GetProduct(guid.Value);
                return product;
            }
            else
            {
                return null;
            }
        }

        protected void btnSubmitProduct_Click(object sender, EventArgs e)
        {
            bool proceedToProductImages = ((CheckBox)frmvProductEdit.FindControl("chkProceedToProductImages")).Checked;
            switch (requestType)
            {
                case "new":
                    frmvProductEdit.InsertItem(true);
                    break;
                case "edit":
                    frmvProductEdit.UpdateItem(true);
                    break;
                default:
                    break;
            }
            if (Page.IsValid)
            {
                if (proceedToProductImages)
                {
                    Response.Redirect(NavigationHelper.ProductImageManager(ViewState["newProductID"].ToString()));
                }
                else
                {
                    Response.Redirect(NavigationHelper.ProductsView(), true);
                }
            }
        }

        protected void btnCancelSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.ProductsView(), true);
        }

        public IList<ProductCategory> cboProductCategory_GetData()
        {
            return categoryData.GetAllCategories();
        }

        public void frmvProductEdit_InsertItem()
        {
            var productToAdd = new Product();
            TryUpdateModel(productToAdd);
            if (ModelState.IsValid)
            {
                var categoryID = ((DropDownList)frmvProductEdit.FindControl("cboProductCategory")).SelectedValue;
                if (null != categoryID)
                {
                    productToAdd.CategoryID = Guid.Parse(categoryID);
                }
                ViewState.Add("newProductID", productData.InsertProduct(productToAdd));
            }
        }

        public void frmvProductEdit_UpdateItem([QueryString]Guid? guid)
        {
            Product product = new Product();
            product.CategoryID = Guid.Parse(((DropDownList)frmvProductEdit.FindControl("cboProductCategory")).SelectedValue);
            if (guid == null)
            {
                ModelState.AddModelError("", "Invalid product has been selected.");
                return;
            }
            if(!productData.IsProductExists(guid.Value))
            {
                ModelState.AddModelError("", "The product you selected no longer exists.");
                return;
            }
            product.ProductID = guid.Value;
            TryUpdateModel(product);
            if (ModelState.IsValid)
            {
                productData.UpdateProduct(product);
            }
        }

        protected void productExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (frmvProductEdit.CurrentMode == FormViewMode.Insert)
            {
                args.IsValid = !productData.IsProductExists(args.Value);
            }
        }
    }
}