using log4net;
using Microsoft.Practices.Prism.Regions;
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
using System.ComponentModel;
using Microsoft.Practices.Prism.PubSubEvents;

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

                EventAggregator eventAggregator = new EventAggregator();
                unityContainer.RegisterInstance<IEventAggregator>(eventAggregator);
                unityContainer.RegisterInstance<IWebservice>(webService);

                var regionManager = new RegionManager();
                regionManager.Regions.Add(new Region() { Name = "MainContentRegion" });
                unityContainer.RegisterInstance<IRegionManager>(regionManager);

                ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

            }
            catch (Exception ex)
            {
                logger.Error("Error in app constructor", ex);
            }
        }
    }
}
