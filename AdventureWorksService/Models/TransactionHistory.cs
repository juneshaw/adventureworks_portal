using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorksService.Models
{
    public class TransactionHistory
    {
        public string TransactionDate { get; set; }

        public string TransactionType { get; set; }

        public int Quantity { get; set; }

        public decimal ActualCost { get; set; }
    }
}
