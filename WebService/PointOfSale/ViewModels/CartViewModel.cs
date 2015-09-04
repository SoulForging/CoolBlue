using System.Collections.Generic;
using DataContracts.Models;
using PointOfSale.DataStructures;
using PointOfSale.Interfaces;

namespace PointOfSale.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private IEnumerable<CartItem> currentCart;
        private Customer currentCustomer;

        public CartViewModel(IWebservice webService) : base(webService)
        {
        }

        public CartViewModel(IWebservice webService, IEnumerable<CartItem> currentCart, Customer currentCustomer) : this(webService)
        {
            this.currentCart = currentCart;
            this.currentCustomer = currentCustomer;
        }
    }
}
