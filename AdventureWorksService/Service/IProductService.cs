using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AdventureWorksService.Models;

namespace AdventureWorksService.Service
{
    /// <summary>
    /// Interface for the Product service
    /// </summary>
    public interface IProductService
    {       
        PageOfList<ProductSearchItem> SearchProducts(int pageNumber, ProductSearchSortColumn sortColumn, SortDirection sortDirection, int numRecordsInPage, string searchCriteria);        

        Product GetProduct(int productId);

        IEnumerable<ProductModel> GetAvailableProductModels();

        IEnumerable<ProductSubcategory> GetAvailableProductSubcategories();

        OperationStatus UpdateProduct(Product productViewModel);

        TransactionHistoryPageOfList GetTransactionHistory(int productId, int numRecordsInPage, int pageNumber);
    }
}
