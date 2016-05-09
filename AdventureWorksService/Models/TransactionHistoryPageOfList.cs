using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorksService.Models
{
    public class TransactionHistoryPageOfList : PageOfList<TransactionHistory>
    {
        public int ProductId { get; set; }

        public TransactionHistoryPageOfList(IEnumerable<TransactionHistory> transactionHistoryVMCollection, int pageIndex, int pageSize, int totalItemCount)
            : base(transactionHistoryVMCollection, pageIndex, pageSize, totalItemCount)
        {
        }
    }
}
