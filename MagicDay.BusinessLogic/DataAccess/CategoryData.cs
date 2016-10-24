using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicDay.DataModel;


namespace MagicDay.BusinessLogic.DataAccess
{
    public class CategoryData
    {
        public void InsertCategory(ProductCategory category)
        {
            using (var dataModel = new MagicDayEntities())
            {
                category.CategoryID = Guid.NewGuid();
                dataModel.ProductCategories.Add(category);
                dataModel.SaveChanges();
            }
        }

        public ProductCategory GetCategory(Guid categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var editCategory = dataModel.ProductCategories.Find(categoryID);
                return (editCategory != null) ? editCategory : new ProductCategory();
            }
        }

        public IList<CategoryView> GetCategoriesByPage(int startRowIndex, int maximumRows)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var pagedCategories = dataModel.CategoryViews
                    .OrderBy(c => c.Category)
                    .Skip(startRowIndex)
                    .Take(maximumRows)
                    .ToList();
                return pagedCategories;
            }
        }

        public IList<ProductCategory> GetAllCategories()
        {
            using (var dataModel = new MagicDayEntities())
            {
                var allCategories = (from categories in dataModel.ProductCategories
                                     orderby categories.CategoryName ascending
                                     select categories)
                                     .ToList();
                return allCategories;
            }
        }

        public bool DeleteCategory(Guid categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var categoryToRemove = dataModel.ProductCategories.Find(categoryID);
                bool hasChildCategory = dataModel.ProductCategories.Any(c => c.ParentCategoryID == categoryID);
                bool hasProductAttached = dataModel.Products.Any(p => p.CategoryID == categoryID);

                if (hasChildCategory || hasProductAttached)
                {
                    return false;
                }
                dataModel.ProductCategories.Remove(categoryToRemove);
                dataModel.SaveChanges();
                return true;
            }
        }

        public void UpdateCategory(ProductCategory category)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var categoryToUpdate = dataModel.ProductCategories.Find(category.CategoryID);
                dataModel.Entry(categoryToUpdate).CurrentValues.SetValues(category);
                dataModel.SaveChanges();
            }
        }

        public bool IsCategoryExists(string categoryName)
        {
            using (var dataModel = new MagicDayEntities())
            {
                return dataModel.ProductCategories.Any(category => category.CategoryName == categoryName);
            }
        }

        public bool IsCategoryExists(Guid categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                bool isCategoryExists = dataModel.ProductCategories.Any(category => category.CategoryID == categoryID);
                return isCategoryExists;
            }
        }

        private void GatherDescendants(ProductCategory category, ref List<Guid> exclusionList)
        {
            if (null != category && category.CategoryParentToChild.Count > 0)
            {
                foreach (var child in category.CategoryParentToChild)
                {
                    //turn on/off adding to list while testing parent validity method
                    exclusionList.Add(child.CategoryID);
                    GatherDescendants(child, ref exclusionList);
                }
            }
        }

        public IList<ProductCategory> GetValidParentCategories(Guid? categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                List<Guid> exclusionList = new List<Guid>();
                ProductCategory selectedCategory = null;

                var allCategories = (from category in dataModel.ProductCategories
                                     select category).ToList();
                if (null != categoryID)
                {
                    //turn on/off adding to list while testing parent validity method
                    exclusionList.Add(categoryID.Value);
                    selectedCategory = allCategories.Where(c => c.CategoryID == categoryID).FirstOrDefault();
                }
                GatherDescendants(selectedCategory, ref exclusionList);
                var categories = (from category in allCategories
                                  where !exclusionList.Contains(category.CategoryID)
                                  select category).ToList();
                return categories;
            }
        }

        public int GetNumberOfCategories()
        {
            using (var dataModel = new MagicDayEntities())
            {
                return dataModel.CategoryViews.Count();
            }
        }

        private bool ValidateParentCategorySelection(Guid categoryID, ProductCategory selectedParent)
        {
            if (null == selectedParent.ParentCategoryID)
            {
                return true;
            }
            else
            {
                if (selectedParent.ParentCategoryID == categoryID)
                {
                    return false;
                }
                else
                {
                    return ValidateParentCategorySelection(categoryID, selectedParent.CategoryChildToParent);
                }
            }
        }
        public bool IsParentCategoryValid(Guid categoryID, Guid parentCategoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                ProductCategory selectedParent = dataModel.ProductCategories.Find(parentCategoryID);
                return ValidateParentCategorySelection(categoryID, selectedParent);
            }
        }
    }
}


