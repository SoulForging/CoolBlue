using System.Collections.Generic;
using DataContracts.Models;
using PointOfSale.DataStructures;
using PointOfSale.Interfaces;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace PointOfSale.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
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

        private Customer currentCustomer;

        public CartViewModel(IWebservice webService, IEnumerable<CartItem> currentCart, Customer currentCustomer) : base(webService)
        {
            this.MyCart = new ObservableCollection<CartItem>(currentCart);
            this.currentCustomer = currentCustomer;
        }

        private DelegateCommand sendInvoice;

        public DelegateCommand SendInvoiceCommand
        {
            get
            {
                return sendInvoice ?? (sendInvoice = new DelegateCommand(OnSendInvoice));
            }
        }

        public void OnSendInvoice()
        {
            ServiceLocator.Current.GetInstance<IMessaging>().SendMessage(currentCustomer, "Email notification");
        }

    }
}
