using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContracts.Models
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}