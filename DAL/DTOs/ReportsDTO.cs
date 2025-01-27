using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class TopSellingProduct
    {
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
    }

    public class ActiveUser
    {
        public string UserName { get; set; }
        public int OrdersPlaced { get; set; }
    }
}
