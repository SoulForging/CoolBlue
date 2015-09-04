using DataContracts.Models;
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
            using (var client = new HttpClient())
            {
                SetupClient(client);

                HttpResponseMessage response = await client.GetAsync("api/products");

                if (response.IsSuccessStatusCode)
                    toReturn = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return toReturn;
        }

        public async Task<IEnumerable<Product>> SearchProducts(string criteria)
        {
            IEnumerable<Product> toReturn = null;
            using (var client = new HttpClient())
            {
                SetupClient(client);

                HttpResponseMessage response = await client.GetAsync("api/products/" + criteria);

                if (response.IsSuccessStatusCode)
                    toReturn = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return toReturn;
        }
    }
}
