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
    public partial class MultiSelectJumpListPage : ContentPage
    {
        public MultiSelectJumpListVM msjlvm { get; set; }
        public MultiSelectJumpListPage()
        {
            msjlvm = new MultiSelectJumpListVM(Navigation);
            InitializeComponent();
            msjlvm.IsLoading = "Not Loading";
            BindingContext = msjlvm;

            //var getSelectedItemsButton = new ToolbarItem()
            //{
            //    Text = "Get Items"
            //};
            //getSelectedItemsButton.SetBinding(ToolbarItem.CommandProperty, "GetSelectedItemsCommand");
            //ToolbarItems.Add(getSelectedItemsButton);
        }
    }
    public class MultiSelectJumpListVM : _BaseViewModel
    {
        public MultiSelectJumpListVM(INavigation navigation) : base(navigation)
        {
            MultiSelectJumpList = MockData();

            MultiSelectJumpList = new ObservableCollection<MultiSelectJumpListItemModel>(MultiSelectJumpList.Distinct().ToList());
            //Items = Items.OrderBy(x => x).ToList();

            MultiSelectJumpListItems = new ObservableCollection<MultiSelectJumpListSelectableItemWrapper<MultiSelectJumpListItemModel>>(MultiSelectJumpList
                .Select(pk => new MultiSelectJumpListSelectableItemWrapper<MultiSelectJumpListItemModel>
                {
                    Item = pk
                }));

            GroupAndSort();

            IsLoading = $"IsLoading{DateTime.Now}";
        }


        private ObservableCollection<MultiSelectJumpListItemModel> multiSelectJumpList;
        private ObservableCollection<MultiSelectJumpListGrouping<string, MultiSelectJumpListItemModel>> multiSelectJumpListGrouped;
        private ObservableCollection<MultiSelectJumpListSelectableItemWrapper<MultiSelectJumpListItemModel>> multiSelectJumpListItems;

        public ObservableCollection<MultiSelectJumpListItemModel> MultiSelectJumpList
        {
            get { return multiSelectJumpList; }
            set
            {
                multiSelectJumpList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MultiSelectJumpListGrouping<string, MultiSelectJumpListItemModel>> MultiSelectJumpListGrouped
        {
            get { return multiSelectJumpListGrouped; }
            set
            {
                multiSelectJumpListGrouped = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MultiSelectJumpListSelectableItemWrapper<MultiSelectJumpListItemModel>> MultiSelectJumpListItems
        {
            get { return multiSelectJumpListItems; }
            set
            {
                if (multiSelectJumpListItems != value)
                {
                    multiSelectJumpListItems = value;
                    OnPropertyChanged();

                }
            }
        }

        private ICommand _multiSelectJumpListGetSelectedItemsCommand;
        public ICommand MultiSelectJumpListGetSelectedItemsCommand
        {
            get
            {
                return _multiSelectJumpListGetSelectedItemsCommand ??
                    (_multiSelectJumpListGetSelectedItemsCommand = new Command(() =>
                    {
                        GetMultiSelectJumpListSelectedItems();
                    }));
            }
        }

        public ObservableCollection<MultiSelectJumpListItemModel> MockData()
        {
            var items = new ObservableCollection<MultiSelectJumpListItemModel>();
            // Mock Data
            #region A Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new MultiSelectJumpListItemModel
                {                    
                    Name = $"Abbacus{i}",
                    IsSelected = true
                });
            }
            #endregion
            #region B Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new MultiSelectJumpListItemModel
                {
                    Name = $"Boycott{i}",
                    IsSelected = false
                });
            }
            #endregion
            #region C Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new MultiSelectJumpListItemModel
                {
                    Name = $"Cat{i}",
                    IsSelected = false
                });
            }
            #endregion
            #region D Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new MultiSelectJumpListItemModel
                {
                    Name = $"Dog{i}",
                    IsSelected = false
                });
            }
            #endregion
            #region E Items
            for (var i = 1; i < 5; i++)
            {
                items.Add(new MultiSelectJumpListItemModel
                {
                    Name = $"Enumerator{i}",
                    IsSelected = false
                });
            }
            #endregion
            return items;
        }

        ObservableCollection<MultiSelectJumpListItemModel> GetMultiSelectJumpListSelectedItems()
        {
            //List<ListItemModel> selected = (from i in JumpListGrouped
            //                                where i.IsSelected
            //                                select i.Item).ToList();
            var selected = new List<MultiSelectJumpListItemModel>();

            foreach (var group in MultiSelectJumpListGrouped)
            {
                selected.AddRange(group.Where(x => x.IsSelected).ToList());
            }

            return new ObservableCollection<MultiSelectJumpListItemModel>(selected);
        }

        public void GroupAndSort()
        {
            var sorted = from a in MultiSelectJumpList
                         orderby a.Name
                         group a by a.Initial
                into multiselectjumplist
                         select new MultiSelectJumpListGrouping<string, MultiSelectJumpListItemModel>(multiselectjumplist.Key, multiselectjumplist);

            MultiSelectJumpListGrouped = new ObservableCollection<MultiSelectJumpListGrouping<string, MultiSelectJumpListItemModel>>(sorted);
        }
    }

    public class MultiSelectJumpListSelectableCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(MultiSelectJumpListSelectableCell), "");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        private Label lbName;

        public MultiSelectJumpListSelectableCell()
        {
            lbName = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };

            Grid infoLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(4,GridUnitType.Star) },
                    //new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) }
                },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            infoLayout.Children.Add(lbName, 0, 0);

            var cellWrapper = new Grid
            {
                Padding = 10,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(4,GridUnitType.Star) },
                }
            };

            //var cb = new CheckBox();
            //cellWrapper.Children.Add(cb, 0, 0);
            var sw = new Switch();
            sw.SetBinding(Switch.IsToggledProperty, "IsSelected");

            cellWrapper.Children.Add(infoLayout, 1, 0);
            cellWrapper.Children.Add(sw, 0, 0);

            View = cellWrapper;
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

    public class MultiSelectJumpListHeaderCell : ViewCell
    {
        public MultiSelectJumpListHeaderCell()
        {
            this.Height = 30;
            var title = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center
            };
            //if (AppConfig.IsWindows)
            //{
            //    title.TextColor = Color.Black;
            //}

            title.SetBinding(Label.TextProperty, "Key");

            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 30,
                BackgroundColor = Color.FromRgb(52, 152, 218),
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { title }
            };
        }
    }

    public class MultiSelectJumpListGrouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public MultiSelectJumpListGrouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }

    public class MultiSelectJumpListSelectableItemWrapper<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
    }

    public class MultiSelectJumpListItemModel : IEquatable<MultiSelectJumpListItemModel>
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

        public bool Equals(MultiSelectJumpListItemModel other)
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