using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    /// <summary>
    /// Put shared viewmodel base code here, such as initialising services,
    /// Navigation, and INotifyPropertyChanged helper code
    /// </summary>
    public class _BaseViewModel: INotifyPropertyChanged
    {
        public INavigation Navigation;

        // Navigation in ViewModels in Xamarin Forms
        // http://www.johankarlsson.net/2014/09/navigation-from-viewmodel-using.html
        public _BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
            // init shared services here
        }

        #region Helpers (Property Changed Notifyer for Bindings)
        // MVVM in Xamarin Forms
        // http://www.johankarlsson.net/2014/07/mvvm-in-xamarin-forms.html
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
