using MotoGear.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.DataAccess.InMemory
{
    public class ProductCategoryRepo
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;
        public ProductCategoryRepo()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory productCategory)
        {
            productCategories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCatToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCatToUpdate != null)
            {
                productCatToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCatToDelete = productCategories.Find(p => p.Id == Id);

            if (productCatToDelete != null)
            {
                productCategories.Remove(productCatToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        
    }
}
