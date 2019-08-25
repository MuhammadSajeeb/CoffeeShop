using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Models
{
    public class AccountSales
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string CashierName { get; set; }
        public string CustomerName { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangesAmount { get; set; }

    }
}
