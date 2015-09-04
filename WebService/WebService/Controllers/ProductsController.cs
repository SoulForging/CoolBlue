using Dapper;
using DataContracts.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ProductsController : ApiController
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(ProductsController));

        private IEnumerable<Product> GetProducts(string searchString = null)
        {
            List<Product> toReturn = new List<Product>();
            try
            {
                using (var msSql = DBController.GetDBConnection())
                {
                    DynamicParameters p = new DynamicParameters();
                    if (!string.IsNullOrWhiteSpace(searchString))
                        p.Add("SearchString", searchString);

                    toReturn = msSql.Query<Product>("up_FindProducts", param: p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error retrieving DB products", ex);
            }

            return toReturn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return GetProducts().Take(50);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            IEnumerable<Product> toReturn = null;

            try
            {
                toReturn = GetProducts(name).Take(50).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error Retrieving products by name: [{0}]", name), ex);
            }

            return toReturn;
        }
    }
}
