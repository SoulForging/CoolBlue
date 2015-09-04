using DataContracts.Models;
using Microsoft.Practices.ServiceLocation;
using PointOfSale.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private IEnumerable<Product> searchResults;
        public IEnumerable<Product> SearchResults
        {
            get { return searchResults; }
            set
            {
                if (searchResults == value)
                    return;

                searchResults = value;
                OnPropertyChanged();
            }
        }


        public ProductsViewModel()
        {
            Initialize();
        }

        private async void Initialize()
        {
            try
            {
                var webService = ServiceLocator.Current.GetInstance<IWebservice>();
                SearchResults = await webService.SearchProducts("Tom");//.GetAllProducts();
            }
            catch (Exception ex)
            {
                logger.Error("Error initializing ProductsViewModel", ex);
            }
        }

        //private async void OnSearch()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:58781/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync("api/products");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            SearchResults = await response.Content.ReadAsAsync<IEnumerable<Product>>();
        //        }

        //        //DataContractJsonSerializer
        //        //Json.Net
        //        //JsonConvert
        //    }
        //}
    }
}
