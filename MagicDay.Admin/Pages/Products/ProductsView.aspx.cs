using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MagicDay.BusinessLogic.ImageTasks;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.Entity;
using System.Web.ModelBinding;
using System.Data.Entity.Infrastructure;
using MagicDay.BusinessLogic.DataAccess;
using MagicDay.DataModel;
using MagicDay.BusinessLogic.General;

namespace MagicDay.Admin
{
    public partial class ProductsView : System.Web.UI.Page
    {
        private ProductData productData = new ProductData();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.ProductsEditNew());
        }

        public IList<ProductView> productsGrid_GetData(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            maximumRows = productsGrid.PageSize;
            startRowIndex = productsGrid.PageIndex * maximumRows;
            totalRowCount = productData.GetNumberOfProducts();
            return productData.GetProductsByPage(startRowIndex, maximumRows);
        }

        public void productsGrid_DeleteItem()
        {
            Guid productID = (Guid)productsGrid.SelectedDataKey.Value;
            if (productData.DeleteProduct(productID))
            {
                Page.DataBind();
            }
            else
            {
                ModelState.AddModelError("", "Unable to remove product.");
                productsGrid.SelectedIndex = -1;
            }
        }

        protected void productsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productsGrid.PageIndex = e.NewPageIndex;
        }

        protected void productsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            productsGrid.SelectedIndex = e.RowIndex;
        }

        protected void productsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            productsGrid.SelectedIndex = e.NewEditIndex;
            Guid productID = (Guid)productsGrid.SelectedDataKey.Value;
            if(productData.IsProductExists(productID))
            {
            Response.Redirect(NavigationHelper.ProductsEdit(productsGrid.SelectedDataKey.Value.ToString()));
            }
            else
            {
                ModelState.AddModelError("", "Product no longer exists.");
            }
        }

        protected void btnNumberOfImages_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(NavigationHelper.ProductImageManager(((LinkButton)sender).CommandArgument.ToString()));

        }
    }
}
