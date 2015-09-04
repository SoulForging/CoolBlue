using DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class SalesCombinationsController : ApiController
    {
        SalesCombination[] salesCombinations = new SalesCombination[]
        {
            new SalesCombination { SalesCombinationID = 1, MainProductID = 1, SubProductID = 2, Discount = 2 },
            new SalesCombination { SalesCombinationID = 2, MainProductID = 1, SubProductID = 3, Discount = 2 },
            new SalesCombination { SalesCombinationID = 3, MainProductID = 3, SubProductID = 4, Discount = 1.25m }
        };

        public IEnumerable<SalesCombination> GetAllSalesCombinations()
        {
            return salesCombinations;
        }

        public IEnumerable<SalesCombination> GetAllSalesCombinationsByProduct(int productID)
        {
            var toReturn = salesCombinations.Where(s => s.MainProductID == productID);

            return toReturn;
        }

        public IHttpActionResult GetSalesCombination(int id)
        {
            var toReturn = salesCombinations.FirstOrDefault(s => s.SalesCombinationID == id);

            if (toReturn == null)
                return NotFound();

            return Ok(toReturn);
        }
    }
}
