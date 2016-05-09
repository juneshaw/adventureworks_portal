using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

using AdventureWorksService.Models;
using AdventureWorksService.Entities;

namespace AdventureWorksService.Service
{
    /// <summary>
    /// Implementation of the Product Service
    /// </summary>
    public class ProductService : IProductService
    {        
        /// <summary>
        /// Search the Products database
        /// </summary>
        /// <param name="pageNumber">The requested page number</param>
        /// <param name="sortColumn">The requested sort column</param>
        /// <param name="sortDirection">The request sort direction</param>
        /// <param name="numRecordsInPage">Number of records per page</param>
        /// <param name="searchCriteria">The search criteria</param>
        /// <returns></returns>
        public PageOfList<Models.ProductSearchItem> SearchProducts(int pageNumber, ProductSearchSortColumn sortColumn, SortDirection sortDirection, int numRecordsInPage, string searchCriteria)
        {
            try
            {
                // Determine the starting row
                int startRow;
                int totalItemCount = 0;

                if (numRecordsInPage == -1)
                    startRow = 0;
                else
                    startRow = (pageNumber - 1) * numRecordsInPage;

                PageOfList<Models.ProductSearchItem> productSearchResults;

                // Connect to the AdventureWords data source using the DB context
                using (AdventureWorksEntities _db = new AdventureWorksEntities())
                {
                    // Use LINQ to search the products data source
                    List<Entities.ProductSearchItem> products = (from s in _db.ProductSearchItems where s.ProductName.Contains(searchCriteria) || s.ProductNumber.Contains(searchCriteria) select s).ToList();

                    // Apply the sorting using the requested sort column and direction
                    if (sortDirection == SortDirection.Ascending)
                    {
                        if (sortColumn == ProductSearchSortColumn.Name)
                            products = products.OrderBy(p => p.ProductName).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Number)
                            products = products.OrderBy(p => p.ProductNumber).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Subcategory)
                            products = products.OrderBy(p => p.ProductSubcategory).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Category)
                            products = products.OrderBy(p => p.ProductCategory).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Model)
                            products = products.OrderBy(p => p.ProductModel).ToList();
                    }
                    else
                    {
                        if (sortColumn == ProductSearchSortColumn.Name)
                            products = products.OrderByDescending(p => p.ProductName).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Number)
                            products = products.OrderByDescending(p => p.ProductNumber).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Subcategory)
                            products = products.OrderByDescending(p => p.ProductSubcategory).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Category)
                            products = products.OrderByDescending(p => p.ProductCategory).ToList();
                        else if (sortColumn == ProductSearchSortColumn.Model)
                            products = products.OrderByDescending(p => p.ProductModel).ToList();
                    }

                    totalItemCount = products.Count();

                    // Apply the paging
                    if (numRecordsInPage != -1)
                        products = products.Skip(startRow).Take(numRecordsInPage).ToList();

                    // Convert the Product Entity collection to Product UI Model collection
                    List<Models.ProductSearchItem> productSearchVMCollection = new List<Models.ProductSearchItem>();

                    foreach (Entities.ProductSearchItem product in products)
                    {
                        Models.ProductSearchItem productView = new Models.ProductSearchItem
                        {
                            ProductId = product.ProductId,
                            Name = product.ProductName,
                            Number = product.ProductNumber,
                            Subcategory = product.ProductSubcategory,
                            Category = product.ProductCategory,
                            Model = product.ProductModel
                        };

                        productSearchVMCollection.Add(productView);
                    }

                    productSearchResults = new PageOfList<Models.ProductSearchItem>(productSearchVMCollection, pageNumber, numRecordsInPage, totalItemCount);
                }

                return productSearchResults;
            }
            catch (Exception e)
            {
                // Send the exception to the user interface
                throw e;
            }
        }

        /// <summary>
        /// GetProduct
        /// Get the details of the product identified by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Models.Product GetProduct(int productId)
        {
            try
            {
                Models.Product productViewModel = new Models.Product();

                // Connect to the AdventureWorks data source using the DB context
                using (AdventureWorksEntities _db = new AdventureWorksEntities())
                {
                    Entities.Product product = _db.Products.First(p => p.ProductID == productId);

                    if (product != null)
                    {
                        // Populate the Product UI model with data from the Product entity collection
                        productViewModel = new Models.Product()
                        {
                            ProductId = product.ProductID,
                            ProductName = product.Name,
                            ProductNumber = product.ProductNumber,
                            StandardCost = product.StandardCost,
                            ListPrice = product.ListPrice,
                            ProductModel = product.ProductModelID.Value,
                            ProductSubcategory = product.ProductSubcategoryID.Value
                        };
                    }
                }

                return productViewModel;
            }
            catch (Exception e)
            {
                // Send the exception to the UI
                throw e;
            }
        }

        /// <summary>
        /// GetAvailableProductModels
        /// Return a collection of available product models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.ProductModel> GetAvailableProductModels()
        {
            try
            {
                List<Models.ProductModel> productModelViewModelCollection = new List<Models.ProductModel>();

                // Connect to the AdventureWorks data source using the db context and get the product models
                using (AdventureWorksEntities _db = new AdventureWorksEntities())
                {
                    List<Entities.ProductModel> productModels = (from m in _db.ProductModels select m).ToList();

                    // Convert the product model entities to UI model
                    foreach (Entities.ProductModel productModel in productModels)
                    {
                        productModelViewModelCollection.Add(new Models.ProductModel
                        {
                            ProductModelId = productModel.ProductModelID,
                            ProductModelName = productModel.Name
                        });
                    }
                }

                return productModelViewModelCollection;
            }
            catch (Exception e)
            {
                // Send the exception to the user interface
                throw e;
            }
        }

        /// <summary>
        /// GetAvailableProductSubcategories
        /// Get the available product sub-categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.ProductSubcategory> GetAvailableProductSubcategories()
        {
            try
            {
                List<Models.ProductSubcategory> productSubcategoryViewModelCollection = new List<Models.ProductSubcategory>();

                // Connect to the AdventureWorks data source and get the available product sub-categories
                using (AdventureWorksEntities _db = new AdventureWorksEntities())
                {
                    List<Entities.ProductSubcategory> productSubcategories = (from s in _db.ProductSubcategories select s).ToList();

                    foreach (Entities.ProductSubcategory productSubcategory in productSubcategories)
                    {
                        productSubcategoryViewModelCollection.Add(new Models.ProductSubcategory
                        {
                            ProductSubcategoryId = productSubcategory.ProductSubcategoryID,
                            ProductSubcategoryName = productSubcategory.Name
                        });
                    }
                }

                return productSubcategoryViewModelCollection;
            }
            catch (Exception e)
            {
                // Send the exception to the user interface
                throw e;
            }
        }

        /// <summary>
        /// UpdateProduct
        /// Update the product in the database
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public OperationStatus UpdateProduct(Models.Product productViewModel)
        {
            try
            {
                // Use the ValidationContext to validate the Product model against the product data annotations
                // before saving it to the database
                var validationContext = new ValidationContext(productViewModel, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

                // If there any exception return them in the return result
                if (!isValid)
                {
                    OperationStatus opStatus = new OperationStatus();
                    opStatus.Success = false;

                    foreach (ValidationResult message in validationResults)
                    {
                        opStatus.Messages.Add(message.ErrorMessage);
                    }

                    return opStatus;
                }
                else
                {
                    // Otherwise connect to the data source using the db context and save the 
                    // product model to the database
                    using (AdventureWorksEntities _db = new AdventureWorksEntities())
                    {
                        Entities.Product product = _db.Products.First(p => p.ProductID == productViewModel.ProductId);

                        product.Name = productViewModel.ProductName;
                        product.ProductNumber = productViewModel.ProductNumber;
                        product.StandardCost = productViewModel.StandardCost;
                        product.ListPrice = productViewModel.ListPrice;
                        product.ProductModelID = productViewModel.ProductModel;
                        product.ProductSubcategoryID = productViewModel.ProductSubcategory;

                        // Call the update function of the Products entity
                        _db.SaveChanges();
                    }

                    return new OperationStatus { Success = true };
                }
            }
            catch (Exception e)
            {
                return OperationStatus.CreateFromException("Error updating product.", e);
            }
        }

        /// <summary>
        /// GetTransactionHistory
        /// Get the transaction history of a given product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="numRecordsInPage"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public TransactionHistoryPageOfList GetTransactionHistory(int productId, int numRecordsInPage, int pageNumber)
        {
            try
            {
                // Determine the starting row
                int startRow;
                int totalItemCount = 0;

                if (pageNumber < 1)
                    pageNumber = 1;

                if (numRecordsInPage == -1)
                    startRow = 0;
                else
                    startRow = (pageNumber - 1) * numRecordsInPage;

                TransactionHistoryPageOfList transactionHistorySearchResults;

                // Connect to the AdventureWorks data source using the db context
                using (AdventureWorksEntities _db = new AdventureWorksEntities())
                {
                    List<Entities.TransactionHistory> transactionHistoryCollection = (from t in _db.TransactionHistories
                                                                                      where t.ProductID == productId
                                                                                      select t).ToList();

                    // Get the total number of records before we trip for paging
                    totalItemCount = transactionHistoryCollection.Count();

                    // Apply the paging
                    if (numRecordsInPage != -1)
                        transactionHistoryCollection = transactionHistoryCollection.Skip(startRow).Take(numRecordsInPage).ToList();

                    List<Models.TransactionHistory> transactionHistoryVMCollection = new List<Models.TransactionHistory>();

                    foreach (Entities.TransactionHistory transactionHistory in transactionHistoryCollection)
                    {
                        transactionHistoryVMCollection.Add(new Models.TransactionHistory
                        {
                            TransactionDate = transactionHistory.TransactionDate.ToShortDateString(),
                            TransactionType = transactionHistory.TransactionType,
                            Quantity = transactionHistory.Quantity,
                            ActualCost = transactionHistory.ActualCost
                        });
                    }

                    transactionHistorySearchResults = new TransactionHistoryPageOfList(transactionHistoryVMCollection, pageNumber, numRecordsInPage, totalItemCount);
                    transactionHistorySearchResults.ProductId = productId;
                }

                return transactionHistorySearchResults;
            }
            catch (Exception e)
            {
                // Send the exception to the user interface
                throw e;
            }
        }
    }
}
