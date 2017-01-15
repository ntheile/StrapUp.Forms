using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class MainControlsPage : ContentPage
    {

        public ObservableCollection<MyPage> Pages { get; set; }

        public MainControlsPage()
        {
            InitializeComponent();

            Pages = new ObservableCollection<MyPage> {
               new MyPage { page="CardView" },
               new MyPage { page="ExpansionPanel" }
            };


            BindingContext = this;
        }

        public async void NavPage(object sender, EventArgs e)
        {
            // Cast Object to listview item then selected item
            ListView list = (ListView)sender;
            MyPage item = list.SelectedItem as MyPage;

            // Dynamically get an instance of the selected page
            Page pageInstance = (Page)Activator.CreateInstance(Type.GetType("TestApp" + "." + item.page + "Page"));
           
            // Navigate
            await this.Navigation.PushAsync(pageInstance);
        }

    }

    public class MyPage
    {
        public string page { get; set; }
    }

}
