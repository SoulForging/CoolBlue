using DataContracts.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ProductsController : ApiController
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(ProductsController));

        Product[] products = new Product[]
        {
            new Product { ProductID = 1, Name = "Tomato Soup", Price = 1 },
            new Product { ProductID = 2, Name = "Yo-yo", Price = 3.75M },
            new Product { ProductID = 3, Name = "Hammer", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products.Take(50);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            IEnumerable<Product> toReturn = null;

            try
            {
                toReturn = products.Where(s => s.Name.Contains(name)).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error Retrieving products by name: [{0}]", name), ex);
            }

            return toReturn;
        }
    }
}
