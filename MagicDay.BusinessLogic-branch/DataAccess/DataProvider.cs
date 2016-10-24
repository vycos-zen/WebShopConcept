using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MagicDay.BusinessLogic.DataAccess
{
    public class DataProvider
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

        public ProductCategory GetCategory(Guid? categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var editCategory = dataModel.ProductCategories.Find(categoryID);
                return (editCategory != null) ? editCategory : new ProductCategory();
            }
        }

        public IList<ProductCategory> GetCategoriesByPage(int startRowIndex, int maximumRows)
        {
            using (var dataModel = new MagicDayEntities())
            {
                //var pagedCategories = (from categories in dataModel.ProductCategories
                //                       orderby categories.CategoryName ascending

                //                       select categories)
                //        .Skip((pageSize - 1) * pageNumber)
                //        .Take(pageSize);

                //return pagedCategories.ToList().AsQueryable();


                var pagedCategories = dataModel.ProductCategories
                    .OrderBy(c => c.CategoryName)
                    .Include("CategoryChildToParent")
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

        private delegate void GatherDescendantsDelegate(GatherDescendantsDelegate recurse, ProductCategory category, ref List<Guid> exclusionList);

        private void GatherDescendants(GatherDescendantsDelegate recurse, ProductCategory category, ref List<Guid> exclusionList)
        {
            if (null != category && category.CategoryParentToChild.Count > 0)
            {
                foreach (var child in category.CategoryParentToChild)
                {
                    exclusionList.Add(child.CategoryID);
                    recurse(recurse, child, ref exclusionList);
                }
            }
        }

        public IList<ProductCategory> GetValidParentCategories(Guid? categoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {

                GatherDescendantsDelegate gatherDescendants = new GatherDescendantsDelegate(GatherDescendants);
                List<Guid> exclusionList = new List<Guid>();
                ProductCategory selectedCategory = null;

                //gets all the categories from the database
                var allCategories = (from category in dataModel.ProductCategories
                                     select category).ToList();
                if (null != categoryID)
                {
                    exclusionList.Add(categoryID.Value);
                    selectedCategory = allCategories.Where(c => c.CategoryID == categoryID).FirstOrDefault();
                }

                //recursive delegate to check for all child items, and place them in exclusion list

                gatherDescendants(gatherDescendants, selectedCategory, ref exclusionList);

                //narrows down the category list to valid items only
                var categories = (from category in allCategories
                                  where !exclusionList.Contains(category.CategoryID)
                                  select category).ToList();
                return categories;
            }
        }

        public int GetNumberOfCategories()
        {
            using (var dataBase = new MagicDayEntities())
            {
                return dataBase.ProductCategories.Count();
            }
        }

        public bool IsParentCategoryValid(Guid categoryID, Guid parentCategoryID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                GatherDescendantsDelegate gatherDescendants = new GatherDescendantsDelegate(GatherDescendants);
                List<Guid> exclusionList = new List<Guid>();
                ProductCategory categoryToCheck = dataModel.ProductCategories.Find(categoryID);
                exclusionList.Add(categoryID);
                gatherDescendants(gatherDescendants, categoryToCheck, ref exclusionList);
                return (!exclusionList.Contains(parentCategoryID));
            }
        }
    }
}


