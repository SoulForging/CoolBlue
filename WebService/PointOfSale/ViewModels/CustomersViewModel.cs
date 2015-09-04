using DataContracts.Models;
using PointOfSale.Interfaces;

namespace PointOfSale.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        private bool existingUser;
        public bool ExistingUser
        {
            get { return existingUser; }
            set
            {
                existingUser = value;
                OnPropertyChanged();

                if (!existingUser)
                    Customer = new Customer();
                else
                    Customer = null;

                OnPropertyChanged("HasCustomer");
                OnPropertyChanged("SaveButtonHeading");
            }
        }

        public bool HasCustomer
        {
            get
            {
                return Customer != null;
            }
        }

        public string SaveButtonHeading
        {
            get
            {
                return ExistingUser ? "Update User" : "Add User";
            }
        }

        private Customer customer;
        public Customer Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }


        public CustomersViewModel(IWebservice webService)
            : base(webService)
        {
            ExistingUser = true;
        }
    }
}
