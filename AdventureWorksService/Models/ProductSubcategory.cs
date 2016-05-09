using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksService.Models
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryId { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductCategoryName { get; set; }
    }
}