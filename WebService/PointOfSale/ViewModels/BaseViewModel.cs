using log4net;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PointOfSale.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(BaseViewModel));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
