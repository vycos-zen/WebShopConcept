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
    public partial class CategoriesEdit : System.Web.UI.Page
    {
        private CategoryData categoryData = new CategoryData();
        private string requestType;

        protected void Page_Load(object sender, EventArgs e)
        {
            requestType = Context.Request.QueryString["action"];
            switch (requestType)
            {
                case "new":
                    frmvCategoryForm.ChangeMode(FormViewMode.Insert);
                    break;
                case "edit":
                    frmvCategoryForm.ChangeMode(FormViewMode.Edit);
                    break;
                default:
                    break;
            }
        }

        public ProductCategory frmvCategoryForm_GetItem([QueryString]Guid? guid)
        {
            var category = new ProductCategory();
            if (frmvCategoryForm.CurrentMode == FormViewMode.Insert)
            {
                return category;
            }
            else if (frmvCategoryForm.CurrentMode == FormViewMode.Edit && null != guid)
            {
                category = categoryData.GetCategory(guid.Value);
                return category;
            }
            else
            {
                return null;
            }
        }
        public void frmvCategoryForm_UpdateItem([QueryString]Guid? guid)
        {
            ProductCategory category = new ProductCategory();
            if (((CheckBox)frmvCategoryForm.FindControl("hasParentCategory")).Checked)
            {
                category.ParentCategoryID = Guid.Parse(((DropDownList)frmvCategoryForm.FindControl("parentCategoryDrop")).SelectedValue);
            }
            else
            {
                category.ParentCategoryID = null;
            }
            if(null == guid)
            {
                ModelState.AddModelError("", "Invalid category has been selected.");
                return;
            }
            if(!categoryData.IsCategoryExists(guid.Value))
            {
                ModelState.AddModelError("", "The category you selected no longer exists.");
                return;
            }
            TryUpdateModel(category);
            if (ModelState.IsValid)
            {
                categoryData.UpdateCategory(category);
                Response.Redirect(NavigationHelper.CategoriesView(), true);
            }
        }

        public void frmvCategoryForm_InsertItem()
        {
            var categoryToAdd = new ProductCategory();
            TryUpdateModel(categoryToAdd);
            if (ModelState.IsValid)
            {
                var selectedParentID = ((DropDownList)frmvCategoryForm.FindControl("parentCategoryDrop")).SelectedValue;
                bool hasParentChecked = ((CheckBox)frmvCategoryForm.FindControl("hasParentCategory")).Checked;
                if (selectedParentID != null && hasParentChecked)
                {
                    categoryToAdd.ParentCategoryID = Guid.Parse(selectedParentID);
                }
                categoryData.InsertCategory(categoryToAdd);
            }
        }

        protected void frmvCategoryForm_ItemCreated(object sender, EventArgs e)
        {
            CheckBox chkHasParent = (CheckBox)frmvCategoryForm.FindControl("hasParentCategory");
            if (chkHasParent != null)
            {
                chkHasParent.CheckedChanged += new EventHandler(hasParentCategory_CheckedChanged);
                chkHasParent.Checked = false;
            }
        }
        public IList<ProductCategory> parentCategoryDrop_GetData()
        {
            var categoryID = (Guid?)frmvCategoryForm.DataKey.Value;
            if (categoryID == null)
            {
                return categoryData.GetAllCategories();
            }
            return categoryData.GetValidParentCategories(categoryID);
        }
        protected void parentCategoryDrop_DataBound(object sender, EventArgs e)
        {
            if (frmvCategoryForm.SelectedValue == null)
            {
                return;
            }
            Guid? categoryID = (Guid?)frmvCategoryForm.DataKey.Value;
            if (categoryID != null)
            {
                var activeCategory = frmvCategoryForm.DataItem as ProductCategory;
                ((DropDownList)sender).SelectedValue = activeCategory.ParentCategoryID.ToString();
            }
        }
        protected void categoryExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (frmvCategoryForm.CurrentMode == FormViewMode.Insert)
            {
                args.IsValid = !categoryData.IsCategoryExists(args.Value);
            }
        }
        protected void hasParentCategory_CheckedChanged(object sender, EventArgs e)
        {
            frmvCategoryForm.FindControl("parentCategoryPanel").Visible = ((CheckBox)sender).Checked;
        }
        protected void btnSubmitCategory_Click(object sender, EventArgs e)
        {
                switch (requestType)
                {
                    case "new":
                        frmvCategoryForm.InsertItem(true);
                        break;
                    case "edit":
                        frmvCategoryForm.UpdateItem(true);
                        break;
                    default:
                        break;
                }
                if (Page.IsValid)
                {
                    Response.Redirect(NavigationHelper.CategoriesView(), true);
                }
        }

        protected void btnCancelSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(NavigationHelper.CategoriesView(), true);
        }

        protected void parentCategoryValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (frmvCategoryForm.CurrentMode == FormViewMode.Edit)
            {
                Guid parentCategoryID = Guid.Parse(((DropDownList)frmvCategoryForm.FindControl("parentCategoryDrop")).SelectedValue);
                Guid categoryID = (Guid)frmvCategoryForm.DataKey.Value;
                args.IsValid = categoryData.IsParentCategoryValid(categoryID, parentCategoryID);
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}