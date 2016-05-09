using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorksService.Models
{
    /// <summary>
    /// Product
    /// The Product UI Model class
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The Product Name is required")]
        [StringLength(50, ErrorMessage = "The Product Name is too long")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Product Number is required")]
        [StringLength(25, ErrorMessage = "The Product Number is too long")]
        public string ProductNumber { get; set; }

        [Required(ErrorMessage = "The Standard Cost is required")]
        [DataType(DataType.Currency, ErrorMessage = "The Standard Cost is not in the correct format")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "The Standard Cost is not in the correct format")]
        [Range(0, 5000, ErrorMessage = "The Standard Cost must be between 0 and 5000")]
        public decimal StandardCost { get; set; }

        [Required(ErrorMessage = "The List Price is required")]
        [DataType(DataType.Currency, ErrorMessage = "The List Price is not in the correct format")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "The List Price is not in the correct format")]
        [Range(0, 5000, ErrorMessage = "The List Price must be between 0 and 5000")]
        public decimal ListPrice { get; set; }
        
        [Required(ErrorMessage = "The Product Model is required")]
        public int ProductModel { get; set; }

        [Required(ErrorMessage = "The Product Subcategory is required")]
        public int ProductSubcategory { get; set; }
    }
}