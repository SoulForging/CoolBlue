using DataContracts.Models;
using Microsoft.Practices.Prism.Commands;
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


        private string userSearchText;
        public string UserSearchText
        {
            get { return userSearchText; }
            set
            {
                userSearchText = value;
                OnPropertyChanged();
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
                OnPropertyChanged("HasCustomer");
            }
        }

        private DelegateCommand addUpdateCustomerCommand;
        public DelegateCommand AddUpdateCustomerCommand
        {
            get
            {
                return addUpdateCustomerCommand ?? (addUpdateCustomerCommand = new DelegateCommand(OnAddUpdateCustomer));
            }
        }


        private DelegateCommand searchForCustomerCommand;
        public DelegateCommand SearchForCustomerCommand
        {
            get
            {
                return searchForCustomerCommand ?? (searchForCustomerCommand = new DelegateCommand(OnSearchForCustomer));
            }
        }


        public CustomersViewModel(IWebservice webService)
            : base(webService)
        {
            ExistingUser = true;
        }

        public async void OnSearchForCustomer()
        {
            var result = await webService.SearchForCustomer(UserSearchText);

            if (result != null)
                Customer = result;
            
            //else show no user found
        }

        public async void OnAddUpdateCustomer()
        {
            if (ExistingUser)
            {
                var resultTwo = await webService.UpdateCustomer(Customer);
                //TODO: messagedialog.mbox updated!
            }
            else
            {
                var result = await webService.AddCustomer(Customer);
                Customer = result;
            }
        }

    }
}
