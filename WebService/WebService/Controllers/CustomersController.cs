using Dapper;
using DataContracts.Models;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers
{
    public class CustomersController : ApiController
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(CustomersController));

        public Customer PostAddCustomer(Customer toAdd)
        {
            return AddOrUpdateCustomer(toAdd);
        }

        public bool PutUpdateCustomer(Customer toUpdate)
        {
            return AddOrUpdateCustomer(toUpdate) != null;
        }

        private Customer AddOrUpdateCustomer(Customer toAddOrUpdate)
        {
            Customer toReturn = null;
            try
            {
                using (var msSql = DBController.GetDBConnection())
                {
                    DynamicParameters p = new DynamicParameters();
                    p.Add("CustomerID", toAddOrUpdate.CustomerID);
                    p.Add("FirstName", toAddOrUpdate.FirstName);
                    p.Add("LastName", toAddOrUpdate.LastName);
                    p.Add("MiddleName", toAddOrUpdate.MiddleName);
                    p.Add("EmailAddress", toAddOrUpdate.EmailAddress);
                    p.Add("Street", toAddOrUpdate.Street);
                    p.Add("Housenumber", toAddOrUpdate.Housenumber);
                    p.Add("PostalCode", toAddOrUpdate.PostalCode);
                    p.Add("City", toAddOrUpdate.City);

                    toReturn = msSql.Query<Customer>("up_AddOrUpdateCustomer", param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error retrieving DB customer", ex);
            }

            return toReturn;
        }

        public Customer GetCustomerByName(string name)
        {
            return FindCustomer(name);
        }

        private Customer FindCustomer(string name)
        {
            Customer toReturn = null;
            try
            {
                using (var msSql = DBController.GetDBConnection())
                {
                    DynamicParameters p = new DynamicParameters();
                    p.Add("CustomerName", name);

                    toReturn = msSql.Query<Customer>("up_FindCustomerByName", param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error retrieving DB customer", ex);
            }

            return toReturn;
        }
    }
}
