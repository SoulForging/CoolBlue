using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContracts.Models
{
    public class SalesCombination
    {
        public int SalesCombinationID { get; set; }
        public int MainProductID { get; set; }
        public int SubProductID { get; set; }
        public decimal Discount { get; set; }
    }
}