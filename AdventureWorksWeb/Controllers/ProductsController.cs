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
    /// ProductsController
    /// 
    /// Controller for performing database related operations from the UI against the Products domain
    /// </summary>
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ProductsController : Controller
    {
        const int PAGE_SIZE = 10;

        // Interface to the ProductsService       
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            // Concrete implementation of the products service will be managed by Unity
            _productService = productService;
        }

        //
        // GET: /Products/
        // Default view
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Products/Search/keyword       
        //
        // keyword - keyword that is being searched
        public ActionResult Search(string keyword)
        {           
            // Default sort column and direction
            ProductSearchSortColumn productSearchSortColumn = ProductSearchSortColumn.Name;
            SortDirection productSearchSortDirection = SortDirection.Ascending;

            PageOfList<ProductSearchItem> productSearchResults = _productService.SearchProducts(1, productSearchSortColumn, productSearchSortDirection, 10, keyword);

            if (productSearchResults.TotalItemCount == 0)
                return PartialView("NoSearchResults");
            else
                return PartialView("SearchResultsWithPaging", productSearchResults);
        }

        //
        // GET: /Products/Page/pageNumber/sortSolumn/sortDirection/keyword       
        //
        // Paged method for searching products
        // pageNumber - the number of the page requests
        // sortColum - the request sort
        // sortDirection - the direction of the sort ASC (Ascending) or DESC (Descending)
        // keyword - keyword that is being searched
        public ActionResult Page(int pageNumber, string sortColumn, string sortDirection, string keyword)
        {
            ProductSearchSortColumn productSearchSortColumn;
            SortDirection productSearchSortDirection;

            if (sortColumn == "Name")
                productSearchSortColumn = ProductSearchSortColumn.Name;
            else if (sortColumn == "Number")
                productSearchSortColumn = ProductSearchSortColumn.Number;
            else if (sortColumn == "Subcategory")
                productSearchSortColumn = ProductSearchSortColumn.Subcategory;
            else if (sortColumn == "Category")
                productSearchSortColumn = ProductSearchSortColumn.Category;
            else if (sortColumn == "Model")
                productSearchSortColumn = ProductSearchSortColumn.Model;
            else
                productSearchSortColumn = ProductSearchSortColumn.Name;

            if (sortDirection == "ASC")
                productSearchSortDirection = SortDirection.Ascending;
            else
                productSearchSortDirection = SortDirection.Descending;

            PageOfList<ProductSearchItem> productSearchResults = _productService.SearchProducts(pageNumber, productSearchSortColumn, productSearchSortDirection, PAGE_SIZE, keyword);

            return PartialView("SearchResults", productSearchResults);
        }

        //
        // GET: /Products/Details/5
        //
        // Get the details of a product
        public ActionResult Details(int id)
        {
            Product productViewModel = _productService.GetProduct(id);

            IEnumerable<ProductModel> productModels = _productService.GetAvailableProductModels();
            IEnumerable<ProductSubcategory> productSubcategories = _productService.GetAvailableProductSubcategories();

            ViewData["ProductModels"] =
                from p in productModels
                select new SelectListItem
                {
                    Text = p.ProductModelName,
                    Value = p.ProductModelId.ToString()
                };

            ViewData["ProductSubcategories"] =
                from p in productSubcategories
                select new SelectListItem
                {
                    Text = p.ProductSubcategoryName,
                    Value = p.ProductSubcategoryId.ToString()
                };

            return PartialView(productViewModel);
        }

        // POST: /Products/Update/5
        //
        // Update the product
        [HttpPost]
        public ActionResult Update(Product productViewModel)
        {        
            OperationStatus opStatus = _productService.UpdateProduct(productViewModel);
            return Json(opStatus);
        }        
    }
}
