using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AdventureWorksService.Service;
using AdventureWorksService.Models;

namespace AdventureWorksWeb.Controllers
{
    /// <summary>
    /// TransactionHistoryController
    /// 
    /// Controller for performing database operations from the UI against the TransactionHistory domain model
    /// </summary>
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class TransactionHistoryController : Controller
    {
        const int PAGE_SIZE = 10;

        // Interface to the ProductsService
        IProductService _productService;

        public TransactionHistoryController(IProductService productService)
        {
            // Concrete implemetation of the products service will be managed by Unity
            _productService = productService;
        }

        //
        // GET: /TransactionHistory/List/5
        //
        // Return the transaction history record for a product
        public ActionResult List(int id)
        {
            TransactionHistoryPageOfList transactionHistorySearchResults = _productService.GetTransactionHistory(id, PAGE_SIZE, 1);

            if (transactionHistorySearchResults.TotalItemCount == 0)
                return PartialView("NoResults");
            else
                return PartialView("ListWithPaging", transactionHistorySearchResults);
        }

        //
        // GET: /TransactionHistory/Page/234/5
        //
        // Return the transaction history record for a product
        public ActionResult Page(int productId, int pageNumber)
        {
            TransactionHistoryPageOfList transactionHistorySearchResults = _productService.GetTransactionHistory(productId, PAGE_SIZE, pageNumber);

            return PartialView("List", transactionHistorySearchResults);
        }
    }
}
