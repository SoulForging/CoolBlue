using Dapper;
using DataContracts.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class SalesCombinationsController : ApiController
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(ProductsController));

        private IEnumerable<SalesCombination> GetSalesCombinationsWithProductID(int productID)
        {
            List<SalesCombination> toReturn = new List<SalesCombination>();
            try
            {
                using (var msSql = DBController.GetDBConnection())
                {
                    DynamicParameters p = new DynamicParameters();
                    p.Add("ProductID", productID);

                    toReturn = msSql.Query<SalesCombination>("up_FindSalesCombinationWithProductID", param: p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error finding sales combinations", ex);
            }

            return toReturn;
        }

        public IEnumerable<SalesCombination> GetAllSalesCombinations()
        {
            return GetSalesCombinationsWithProductID(0);
        }

        public IEnumerable<SalesCombination> GetAllSalesCombinationsByProduct(int productID)
        {
            var toReturn = GetSalesCombinationsWithProductID(productID);

            return toReturn;
        }
    }
}
