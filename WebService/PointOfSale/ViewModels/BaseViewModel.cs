using log4net;
using Microsoft.Practices.ServiceLocation;
using PointOfSale.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PointOfSale.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(BaseViewModel));

        protected IWebservice webService;

        public BaseViewModel(IWebservice webService)
        {
            this.webService = webService;
        }


        //private IWebservice webservice;
        //protected IWebservice webService
        //{
        //    get
        //    {
        //        return webservice ?? (webservice = ServiceLocator.Current.GetInstance<IWebservice>());
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
