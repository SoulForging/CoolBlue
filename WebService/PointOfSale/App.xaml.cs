using log4net;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PointOfSale.Controllers;
using PointOfSale.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(App));

        public App()
        {
            try
            {
                IUnityContainer unityContainer = new UnityContainer();

                var connectionString = System.Configuration.ConfigurationSettings.AppSettings.Get("webService");
                IWebservice webService = new Webservice(connectionString);
                unityContainer.RegisterInstance<IWebservice>(webService);

                ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

            }
            catch (Exception ex)
            {
                logger.Error("Error in app constructor", ex);
            }
        }
    }
}
