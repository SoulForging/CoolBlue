using DataContracts.Models;
using log4net;
using PointOfSale.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    public class Webservice : IWebservice
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(Webservice));

        private string connectionString;

        public Webservice(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void SetupClient(HttpClient client)
        {
            client.BaseAddress = new Uri(connectionString);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> toReturn = null;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.GetAsync("api/products");

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error retrieving all products", ex);
            }

            return toReturn;
        }

        public async Task<IEnumerable<Product>> SearchProducts(string criteria)
        {
            IEnumerable<Product> toReturn = null;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.GetAsync("api/products?name=" + criteria);

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error searching for products: [{0}]", criteria), ex);
            }

            return toReturn;
        }

        public async Task<Product> GetProduct(int ID)
        {
            Product toReturn = null;

            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.GetAsync("api/products/" + ID.ToString());

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<Product>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error loading product {0}", ID), ex);
            }

            return toReturn;
        }

        public async Task<IEnumerable<SalesCombination>> GetSalesCombinations(int ID)
        {
            IEnumerable<SalesCombination> toReturn = null;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.GetAsync("api/salescombinations?productID=" + ID.ToString());

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<IEnumerable<SalesCombination>>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error loading sales combination [{0}]", ID), ex);
            }

            return toReturn;
        }

        public async Task<bool> UpdateCustomer(Customer toUpdate)
        {
            bool success = false;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.PutAsJsonAsync<Customer>("api/customers", toUpdate);

                    success = response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error loading UpdateCustomer [{0}]", toUpdate.CustomerID), ex);
            }

            return success;
        }

        public async Task<Customer> AddCustomer(Customer toAdd)
        {
            Customer toReturn = null;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.PostAsJsonAsync<Customer>("api/customers", toAdd);

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<Customer>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error adding Customer"), ex);
            }

            return toReturn;
        }

        public async Task<Customer> SearchForCustomer(string searchString)
        {
            Customer toReturn = null;
            try
            {
                using (var client = new HttpClient())
                {
                    SetupClient(client);

                    HttpResponseMessage response = await client.GetAsync("api/customers?search=" + searchString);

                    if (response.IsSuccessStatusCode)
                        toReturn = await response.Content.ReadAsAsync<Customer>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error searching for Customer"), ex);
            }

            return toReturn;
        }
    }
}
