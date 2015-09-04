using Microsoft.Practices.ServiceLocation;
using PointOfSale.Interfaces;
using PointOfSale.ViewModels;
using PointOfSale.Views;
using System.Windows;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Products

            var view = new ProductsView();
            var viewModel = new ProductsViewModel(ServiceLocator.Current.GetInstance<IWebservice>());
            view.DataContext = viewModel;
            LetsHackThis.Content = view;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Customers

            var view = new CustomersView();
            var viewModel = new CustomersViewModel(ServiceLocator.Current.GetInstance<IWebservice>());
            view.DataContext = viewModel;
            LetsHackThis.Content = view;
        }
    }
}
