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

        public IEnumerable<SalesCombination> GetAllSalesCombinations()
        {
            return null;
        }

        public IHttpActionResult GetSalesCombination(int id)
        {
            return NotFound();
            //return Ok(null);
        }

    }
}
