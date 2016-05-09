using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorksService.Models
{
    public class ProductSearchItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Subcategory { get; set; }
        public string Category { get; set; }
        public string Model { get; set; }
    }
}