using DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { ProductID = 1, Name = "Tomato Soup", Price = 1 },
            new Product { ProductID = 2, Name = "Yo-yo", Price = 3.75M },
            new Product { ProductID = 3, Name = "Hammer", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
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
                //TODO: Log4net
            }

            return toReturn;
        }
    }
}
