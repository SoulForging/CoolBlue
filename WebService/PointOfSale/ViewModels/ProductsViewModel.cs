using DataContracts.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using PointOfSale.DataStructures;
using PointOfSale.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
                LoadBundleDeals(value);
            }
        }


        private ObservableCollection<SalesDealContainer> salesComboDeals;
        public ObservableCollection<SalesDealContainer> SalesComboDeals
        {
            get { return salesComboDeals; }
            set
            {
                if (salesComboDeals == value)
                    return;

                salesComboDeals = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CartItem> myCart;
        public ObservableCollection<CartItem> MyCart
        {
            get { return myCart; }
            set
            {
                myCart = value;
                OnPropertyChanged();
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                //TODO: Throttle this call.
                OnTextSearch(value);
            }
        }

        private int cartCount;
        public int CartCount
        {
            get { return cartCount; }
            set
            {
                cartCount = value;
                OnPropertyChanged();
            }
        }

        private decimal cartCost;
        public decimal CartCost
        {
            get { return cartCost; }
            set
            {
                cartCost = value;
                OnPropertyChanged();
            }
        }

        public ProductsViewModel(IWebservice webService)
            : base(webService)
        {
            Initialize();
        }

        private async void Initialize()
        {
            try
            {
                MyCart = new ObservableCollection<CartItem>();
                SalesComboDeals = new ObservableCollection<SalesDealContainer>();
                SearchResults = await webService.GetAllProducts();
            }
            catch (Exception ex)
            {
                logger.Error("Error initializing ProductsViewModel", ex);
            }
        }

        private async void OnTextSearch(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                SearchResults = await webService.GetAllProducts();
            else
                SearchResults = await webService.SearchProducts(searchString);
        }

        private DelegateCommand<Product> addToCartCommand;
        public DelegateCommand<Product> AddToCartCommand
        {
            get
            {
                return addToCartCommand ?? (addToCartCommand = new DelegateCommand<Product>(OnAddToCart));
            }
        }

        public void OnAddToCart(Product productToAdd)
        {
            var existingBasket = MyCart.FirstOrDefault(s => s.cartProduct != null && s.cartProduct.ProductID == productToAdd.ProductID);

            if (existingBasket != null)
                existingBasket.AddOne();
            else
                MyCart.Add(new CartItem(productToAdd));

            CartCount = MyCart.Sum(s => s.cartOrderLine.Quantity);
            CartCost = MyCart.Sum(s => s.cartOrderLine.Price);
        }

        private async void LoadBundleDeals(Product productDealToFind)
        {
            salesComboDeals.Clear();
            if (productDealToFind == null)
                return;

            var deals = await webService.GetSalesCombinations(productDealToFind.ProductID);

            foreach (var deal in deals)
            {
                Product subProduct = await getProduct(deal.SubProductID);
                if (subProduct == null) //If the product doesnt exist, the deal cannot exist
                {
                    logger.Warn(string.Format("Product deal [{0}] exists without valid sub-product [{1}]", deal.SalesCombinationID, deal.SubProductID));
                    return;
                }

                SalesComboDeals.Add(new SalesDealContainer(deal, productDealToFind, subProduct));
            }
        }

        private async Task<Product> getProduct(int ID)
        {
            var foundItem = SearchResults.FirstOrDefault(s => s.ProductID == ID);

            if (foundItem == null)
                foundItem = await webService.GetProduct(ID);

            return foundItem;
        }


        private DelegateCommand<SalesDealContainer> addDiscountedItemToCart;
        public DelegateCommand<SalesDealContainer> AddDiscountedItemToCart
        {
            get
            {
                return addDiscountedItemToCart ?? (addDiscountedItemToCart = new DelegateCommand<SalesDealContainer>(OnAddDiscountedItemToCart));
            }
        }

        public void OnAddDiscountedItemToCart(SalesDealContainer itemToAdd)
        {
            var existingBasket = MyCart.FirstOrDefault(s => s.cartDiscountDeal != null && s.cartDiscountDeal.SalesCombination.SalesCombinationID == itemToAdd.SalesCombination.SalesCombinationID);

            if (existingBasket != null)
                existingBasket.AddOne();
            else
                MyCart.Add(new CartItem(itemToAdd));

            CartCount = MyCart.Sum(s => s.cartOrderLine.Quantity);
            CartCost = MyCart.Sum(s => s.cartOrderLine.Price);
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
