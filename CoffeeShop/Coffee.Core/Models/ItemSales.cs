using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Models
{
    public class ItemSales
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Per_Price { get; set; }
        public decimal Sub_Price { get; set; }
        public string Serial { get; set; }

    }
}
