using MagicDay.BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using MagicDay.DataModel;
using MagicDay.BusinessLogic.General;

namespace MagicDay.Admin
{
    public partial class CategoriesView : System.Web.UI.Page
    {
        private CategoryData categoryData = new CategoryData();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            pnlPageError.DataBind();
        }
        public IList<CategoryView> productCategoriesGrid_GetData(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            maximumRows = productCategoriesGrid.PageSize;
            startRowIndex = productCategoriesGrid.PageIndex * maximumRows;
            totalRowCount = categoryData.GetNumberOfCategories();
            return categoryData.GetCategoriesByPage(startRowIndex, maximumRows);
        }
        public void productCategoriesGrid_DeleteItem()
        {
            Guid categoryID = (Guid)productCategoriesGrid.SelectedDataKey.Value;
            if (categoryData.DeleteCategory(categoryID))
            {
                Page.DataBind();
            }
            else
            {
                ModelState.AddModelError("", "Unable to remove category, it has either products, or it is a parent category.");
                productCategoriesGrid.SelectedIndex = -1;
            }
        }
        protected void productCategoriesGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            productCategoriesGrid.SelectedIndex = e.RowIndex;
        }
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.CategoriesEditNew());
        }
        protected void productCategoriesGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            productCategoriesGrid.SelectedIndex = e.NewEditIndex;
            Guid categoryID = (Guid)productCategoriesGrid.SelectedDataKey.Value;
            if (categoryData.IsCategoryExists(categoryID))
            {
            Response.Redirect(NavigationHelper.CategoriesEdit(categoryID.ToString()));
            }
            else
            {
                ModelState.AddModelError("", "Category no longer exists.");
            }
        }

        protected void productCategoriesGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productCategoriesGrid.PageIndex = e.NewPageIndex;
        }
    }
}