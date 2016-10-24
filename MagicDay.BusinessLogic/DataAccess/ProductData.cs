using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicDay.DataModel;
using System.Data.Entity;

namespace MagicDay.BusinessLogic.DataAccess
{
    public class ProductData
    {
        public Product GetProduct(Guid productID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var product = dataModel.Products.Find(productID);
                return (product != null) ? product : new Product();
            }
        }

        public Guid InsertProduct(Product product)
        {
            using (var dataModel = new MagicDayEntities())
            {
                product.ProductID = Guid.NewGuid();
                dataModel.Products.Add(product);
                dataModel.SaveChanges();
                return product.ProductID;
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var productToUpdate = dataModel.Products.Find(product.ProductID);
                dataModel.Entry(productToUpdate).CurrentValues.SetValues(product);
                dataModel.SaveChanges();
            }
        }

        public bool IsProductExists(string productName)
        {
            using (var dataModel = new MagicDayEntities())
            {
                return dataModel.Products.Any(product => product.ProductName == productName);
            }
        }

        public bool IsProductExists(Guid productID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                return dataModel.Products.Any(product => product.ProductID == productID);
            }
        }

        public bool DeleteProduct(Guid productID)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var productToRemove = dataModel.Products.Find(productID);
                if (null == productToRemove)
                {
                    return false;
                }
                dataModel.Products.Remove(productToRemove);
                dataModel.SaveChanges();
                return true;
            }
        }

        public int GetNumberOfProducts()
        {
            using (var dataModel = new MagicDayEntities())
            {
                return dataModel.Products.Count();
            }
        }

        public IList<ProductView> GetProductsByPage(int startRowIndex, int maximumRows)
        {
            using (var dataModel = new MagicDayEntities())
            {
                var pagedProducts = dataModel.ProductViews
                   .OrderBy(p => p.Name)
                   .Skip(startRowIndex)
                   .Take(maximumRows)
                   .ToList();
                return pagedProducts;
            }
        }

  
    }
}
