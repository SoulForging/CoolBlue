using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using PointOfSale.DataStructures;
using PointOfSale.Interfaces;
using PointOfSale.ViewModels;
using PointOfSale.Views;
using System.Windows;
using System.Windows.Controls;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var view = new ProductsView();
            var viewModel = new ProductsViewModel(ServiceLocator.Current.GetInstance<IWebservice>());
            view.DataContext = viewModel;

            //var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            //regionManager.RegisterViewWithRegion("MainContentRegion", typeof(ProductsView));

            //this.regionManager.RegisterViewWithRegion()
            //var productsUri = new Uri("/Views/ProductsView", UriKind.Relative);
            //regionManager.RequestNavigate("MainContentRegion", productsUri);

            //MainContentRegion
            LetsHackThis.Content = view;

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigateScreenEvent>().Subscribe(ScreenNavigate);
        }

        public void ScreenNavigate(NavigateInfo navigationInfo)
        {
            var nextView = GetView(navigationInfo);
            LetsHackThis.Content = nextView;
        }

        private UserControl GetView(NavigateInfo navigationInfo)
        {
            switch (navigationInfo.ScreenName)
            {
                case "SelectCustomer":
                    var view = new CustomersView();
                    var viewModel = new CustomersViewModel(ServiceLocator.Current.GetInstance<IWebservice>(), navigationInfo.CurrentCart);
                    view.DataContext = viewModel;
                    return view;
                case "ProductSelection":
                    var productsView = new ProductsView();
                    var productsViewModel = new ProductsViewModel(ServiceLocator.Current.GetInstance<IWebservice>(), navigationInfo.CurrentCart);
                    productsView.DataContext = productsViewModel;
                    return productsView;
                case "CartCheckout":
                    var cartView = new CartView();
                    var cartViewModel = new CartViewModel(ServiceLocator.Current.GetInstance<IWebservice>(), navigationInfo.CurrentCart, navigationInfo.CurrentCustomer);
                    cartView.DataContext = cartViewModel;
                    return cartView;
            }

            return null;
        }

    }
}
