using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    public partial class JumpListPage : ContentPage
    {
        public JumpListVM jlvm { get; set; }
        public JumpListPage()
        {
            jlvm = new JumpListVM(Navigation);
            InitializeComponent();
            jlvm.IsLoading = "Not Loading";
            BindingContext = jlvm;

            //var getSelectedItemsButton = new ToolbarItem()
            //{
            //    Text = "Get Items"
            //};
            //getSelectedItemsButton.SetBinding(ToolbarItem.CommandProperty, "GetSelectedItemsCommand");
            //ToolbarItems.Add(getSelectedItemsButton);
        }

        private void JumpListItemHolder_Tapped(object sender, EventArgs e)
        {
            ;
            //Use Below To Navigate Somewhere Or DO SOmething
            //var productModel = (ProductModel)sender;
            //var detailsPage = new CustomerProductDetailPage(productModel);
            //await Navigation.PushAsync(detailsPage);
        }
    }
    public class JumpListVM : _BaseViewModel
    {
        public JumpListVM(INavigation navigation) : base(navigation)
        {
            JumpList = MockData();

            JumpList = new ObservableCollection<JumpListItemModel>(JumpList.Distinct().ToList());
            //Items = Items.OrderBy(x => x).ToList();

            GroupAndSort();

            IsLoading = $"IsLoading{DateTime.Now}";
        }


        private ObservableCollection<JumpListItemModel> jumpList;
        private ObservableCollection<JumpListGrouping<string, JumpListItemModel>> jumpListGrouped;

        public ObservableCollection<JumpListItemModel> JumpList
        {
            get { return jumpList; }
            set
            {
                jumpList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<JumpListGrouping<string, JumpListItemModel>> JumpListGrouped
        {
            get { return jumpListGrouped; }
            set
            {
                jumpListGrouped = value;
                OnPropertyChanged();
            }
        }

        private ICommand _jumpListOnTapCommand;
        public ICommand JumpListOnTapCommand
        {
            get
            {
                //Use Below To Navigate To A New Place
                return _jumpListOnTapCommand = null;
                    //??
                    //(_jumpListOnTapCommand = new Command(async () =>
                    //{
                    //    var productModel = (ProductModel)sender;
                    //    var detailsPage = new CustomerProductDetailPage(productModel);
                    //    await Navigation.PushAsync(detailsPage);
                    //}));
            }
        }

        public ObservableCollection<JumpListItemModel> MockData()
        {
            var items = new ObservableCollection<JumpListItemModel>();
            // Mock Data
            #region A Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new JumpListItemModel
                {
                    Name = $"Abbacus{i}"
                });
            }
            #endregion
            #region B Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new JumpListItemModel
                {
                    Name = $"Boycott{i}"
                });
            }
            #endregion
            #region C Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new JumpListItemModel
                {
                    Name = $"Cat{i}"
                });
            }
            #endregion
            #region D Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new JumpListItemModel
                {
                    Name = $"Dog{i}"
                });
            }
            #endregion
            #region E Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new JumpListItemModel
                {
                    Name = $"Enumerator{i}"
                });
            }
            #endregion
            return items;
        }

        public void GroupAndSort()
        {
            var sorted = from a in JumpList
                         orderby a.Name
                         group a by a.Initial
                into jumpListTemp
                         select new JumpListGrouping<string, JumpListItemModel>(jumpListTemp.Key, jumpListTemp);

            JumpListGrouped = new ObservableCollection<JumpListGrouping<string, JumpListItemModel>>(sorted);
        }
    }

    public class JumpListItemCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(JumpListItemCell), "");

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(JumpListItemCell), null);

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private Label lbName;        

        public JumpListItemCell()
        {
            lbName = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };

            var viewLayout = new StackLayout();

            viewLayout.Children.Add(lbName);

            var gestureRecognizer = new TapGestureRecognizer();

            gestureRecognizer.Tapped += (s, e) => {
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(null);
                }
            };

            //You can either set the binding here or you can set the binding in the xaml. You only need one or the other.
            //Xaml for this is below
            SetBinding(CommandProperty, new Binding("JumpListOnTapCommand"));

            viewLayout.GestureRecognizers.Add(gestureRecognizer);

            View = viewLayout;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lbName.Text = Name;
            }
        }
    }

    public class JumpListGrouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public JumpListGrouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }

    public class JumpListItemModel : IEquatable<JumpListItemModel>
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }

        public string Initial
        {
            get
            {
                String value = Name.Length > 0 ? Name.Substring(0, 1) : "";
                return value;
            }
        }

        public bool Equals(JumpListItemModel other)
        {
            bool retval = false;
            if (String.Equals(this.Name, other.Name))
            {
                retval = true;
            }
            return retval;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}