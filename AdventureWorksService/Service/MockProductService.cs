using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AdventureWorksService.Entities;
using AdventureWorksService.Models;

namespace AdventureWorksService.Service
{
    public class MockProductService : IProductService
    {
        public PageOfList<ProductSearchItem> SearchProducts(int pageNumber, ProductSearchSortColumn sortColumn, SortDirection sortDirection, int numRecordsInPage, string searchCriteria)
        {
            List<ProductSearchItem> products = new List<ProductSearchItem>();

            products.Add(new ProductSearchItem
            {
                ProductId = 1,
                Name = "Bike 1",
                Number = "BK-1",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"                
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 2,
                Name = "Bike 2",
                Number = "BK-2",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 3,
                Name = "Bike 3",
                Number = "BK-3",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 4,
                Name = "Bike 4",
                Number = "BK-4",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 5,
                Name = "Bike 5",
                Number = "BK-5",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 6,
                Name = "Bike 6",
                Number = "BK-6",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 7,
                Name = "Bike 7",
                Number = "BK-7",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 8,
                Name = "Bike 8",
                Number = "BK-8",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 9,
                Name = "Bike 9",
                Number = "BK-9",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            products.Add(new ProductSearchItem
            {
                ProductId = 10,
                Name = "Bike 10",
                Number = "BK-10",
                Model = "BK 2000",
                Category = "Bikes",
                Subcategory = "Standard Bikes"
            });

            PageOfList<ProductSearchItem> productSearchResults = new PageOfList<ProductSearchItem>(products, 1, 10, 11);

            return productSearchResults;
        }

        public Product GetProduct(int productId)
        {
            Product productViewModel = new Product();

            productViewModel.ProductId = productId;
            productViewModel.ProductName = "BK1";
            productViewModel.ProductNumber = "BK-1";
            productViewModel.StandardCost = 25.89m;
            productViewModel.ListPrice = 26.00m;
            productViewModel.ProductModel = 1;
            productViewModel.ProductSubcategory = 1;

            return productViewModel;
        }

        public IEnumerable<ProductModel> GetAvailableProductModels()
        {
            List<ProductModel> productModels = new List<ProductModel>();

            productModels.Add(new ProductModel { ProductModelId = 1, ProductModelName = "BK 2000" });
            productModels.Add(new ProductModel { ProductModelId = 2, ProductModelName = "BK 2002" });
            productModels.Add(new ProductModel { ProductModelId = 3, ProductModelName = "BK 2003" });
            productModels.Add(new ProductModel { ProductModelId = 4, ProductModelName = "BK 2004" });
            productModels.Add(new ProductModel { ProductModelId = 5, ProductModelName = "BK 2005" });
            productModels.Add(new ProductModel { ProductModelId = 6, ProductModelName = "BK 2006" });

            return productModels;
        }

        public IEnumerable<ProductSubcategory> GetAvailableProductSubcategories()
        {
            List<ProductSubcategory> productSubcategories = new List<ProductSubcategory>();

            productSubcategories.Add(new ProductSubcategory { ProductCategoryName = "Bikes", ProductSubcategoryId = 1, ProductSubcategoryName = "Standard Bikes" });
            productSubcategories.Add(new ProductSubcategory { ProductCategoryName = "Bikes", ProductSubcategoryId = 2, ProductSubcategoryName = "Specialized Bikes" });
            productSubcategories.Add(new ProductSubcategory { ProductCategoryName = "Bikes", ProductSubcategoryId = 3, ProductSubcategoryName = "Dirt Bikes" });

            return productSubcategories;
        }

        public OperationStatus UpdateProduct(Product productViewModel)
        {
            // Empty implementation

            return new OperationStatus();
        }

        public TransactionHistoryPageOfList GetTransactionHistory(int productId, int numRecordsInPage, int pageNumber)
        {
            int startRow;

            if (pageNumber < 1)
                pageNumber = 1;

            if (numRecordsInPage == -1)
                startRow = 0;
            else
                startRow = (pageNumber - 1) * numRecordsInPage;

            TransactionHistoryPageOfList transactionHistorySearchResults;

            List<TransactionHistory> transactionHistoryCollection = new List<TransactionHistory>();

            transactionHistoryCollection.Add(new TransactionHistory {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistoryCollection.Add(new TransactionHistory
            {
                TransactionDate = DateTime.Now.ToString(),
                TransactionType = "W",
                Quantity = 10,
                ActualCost = 10.9M
            });

            transactionHistorySearchResults = new TransactionHistoryPageOfList(transactionHistoryCollection, pageNumber, numRecordsInPage, transactionHistoryCollection.Count);
            transactionHistorySearchResults.ProductId = productId;            

            return transactionHistorySearchResults;
        } 
    }
}
