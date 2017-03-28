using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using StrapUp.Forms.Controls;

namespace TestApp
{
    public partial class ExpansionPanelPage : ContentPage
    {
        public VM vm { get; set; }
        
        public ExpansionPanelPage()
        {
            vm = new VM(Navigation);
            InitializeComponent();
            vm.IsLoading = "Not Loading";
            BindingContext = vm;

            //StackLayout collapsedContent = new StackLayout();
            //collapsedContent.Children.Add(new Label { Text="item"});
            //StackLayout exContent = new StackLayout();
            //exContent.Children.Add(new Label { Text = "deatils" });

            //ExpansionPanel ep = new ExpansionPanel();
            //{
            //    CollapsedPanel = collapsedContent,
            //    ExpandedPanel = exContent
            //};
            //SL.Children.Add(ep);



        }

        private void OnExpanded(object sender, EventArgs e)
        {
            var a = 1;
        }


      
    }

    public class VM : _BaseViewModel
    {
        
        public VM(INavigation navigation) : base(navigation)
        {
            PassedInMethodCommand = new Command<object>(PassedInMethod);           
            MyItems = new ObservableCollection<MyItem>(MockData());
        }

        private ObservableCollection<MyItem> _myItems;
        public ObservableCollection<MyItem> MyItems
        {
            get { return _myItems; }
            set
            {
                if (_myItems != value)
                {
                    _myItems = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _collapsedText = "collapsed text...";
        public string CollapsedText
        {
            get { return _collapsedText; }
            set
            {
                if (_collapsedText != value)
                {
                    _collapsedText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand PassedInMethodCommand { get; private set; }
        public void PassedInMethod(object sender)
        {
            // do stuff when you click a row
            IsLoading = $"IsLoading{DateTime.Now}";
        }

        public ObservableCollection<MyItem> MockData()
        {
            var items = new ObservableCollection<MyItem>();
            // Mock Data
            for (var i = 1; i < 20; i++)
            {
                var deets = new ObservableCollection<Detail>();
                for (var z = 1; z < 20; z++)
                {
                    deets.Add(new Detail { text1 = $"detail{i} - {z}", text2 = $"detailsecond{i} - {z}" });
                }
                items.Add(new MyItem
                {
                    text = $"text{i}",
                    details = deets,
                    OnRowClick = PassedInMethodCommand
                });
            }
            return items;
        }

    }


    public class MyItem
    {
        public string text { get; set; }
        public ObservableCollection<Detail> details { get; set; }
        public ICommand OnRowClick { get; set; }
    }

    public class Detail
    {
        public string text1 { get; set; }
        public string text2 { get; set; }
    }

}
