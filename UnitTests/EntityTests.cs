using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using AdventureWorksService.Entities;

namespace UnitTests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void SearchProducts()
        {
            string searchCriteria = "bike";

            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                List<ProductSearchItem> products = (from s in _db.ProductSearchItems where s.ProductName.Contains(searchCriteria) || s.ProductNumber.Contains(searchCriteria) select s).ToList();
            }
        }

        [TestMethod]
        public void GetProductModes()
        {
            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                List<ProductModel> productModels = (from m in _db.ProductModels select m).ToList();
            }
        }

        [TestMethod]
        public void GetProductSubcategories()
        {
            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                List<ProductSubcategory> productSubcategories = (from s in _db.ProductSubcategories select s).ToList();
            }
        }

        [TestMethod]
        public void GetProductDetails()
        {
            int productId = 879;

            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                Product product = _db.Products.First(p => p.ProductID == productId);
            }
        }

        [TestMethod]
        public void GetTransactionHistory()
        {
            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                int productId = 879;

                List<TransactionHistory> transactionHistoryCollection = (from t in _db.TransactionHistories
                                                                                  where t.ProductID == productId
                                                                                  select t).ToList();
            }
        }

        [TestMethod]
        public void UpdateProduct()
        {
            int productId = 879;

            using (AdventureWorksEntities _db = new AdventureWorksEntities())
            {
                Product product = _db.Products.First(p => p.ProductID == productId);

                product.Name = "All-Purpose Bike Stand";
                product.ProductNumber = "ST-1401";
                product.StandardCost = 59.46m;
                product.ListPrice = 159.00m;
                product.ProductModelID = 122;
                product.ProductSubcategoryID = 27;

                _db.SaveChanges();
            }
        }
    }
}
