using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContracts.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime EventDate { get; set; }
    }
}