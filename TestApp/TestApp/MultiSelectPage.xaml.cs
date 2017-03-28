using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    public partial class MultiSelectPage : ContentPage
    {
        public MultiSelectVM msvm { get; set; }
        public MultiSelectPage()
        {
            msvm = new MultiSelectVM(Navigation);
            InitializeComponent();
            msvm.IsLoading = "Not Loading";
            BindingContext = msvm;

            //var getSelectedItemsButton = new ToolbarItem()
            //{
            //    Text = "Get Items"
            //};
            //getSelectedItemsButton.SetBinding(ToolbarItem.CommandProperty, "GetSelectedItemsCommand");
            //ToolbarItems.Add(getSelectedItemsButton);
        }
    }
    public class MultiSelectVM : _BaseViewModel
    {
        public MultiSelectVM(INavigation navigation) : base(navigation)
        {
            MultiSelectedItems = MockData();

            MultiSelectedItems = new ObservableCollection<MultiSelectItem>(MultiSelectedItems.Distinct().ToList());
            //Items = Items.OrderBy(x => x).ToList();

            MultiSelectItems = new ObservableCollection<MultiSelectableItemWrapper<MultiSelectItem>>(MultiSelectedItems
                .Select(pk => new MultiSelectableItemWrapper<MultiSelectItem>
                {
                    Item = pk
                }));

            IsLoading = $"IsLoading{DateTime.Now}";
        }


        private ObservableCollection<MultiSelectableItemWrapper<MultiSelectItem>> _multiSelectItems;
        private ObservableCollection<MultiSelectItem> _multiSelectedItems;

        public ObservableCollection<MultiSelectableItemWrapper<MultiSelectItem>> MultiSelectItems
        {
            get { return _multiSelectItems; }
            set
            {
                if (_multiSelectItems != value)
                {
                    _multiSelectItems = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<MultiSelectItem> MultiSelectedItems
        {
            get { return _multiSelectedItems; }
            private set
            {
                if (_multiSelectedItems != value)
                {
                    _multiSelectedItems = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand _multiSelectGetSelectedItemsCommand;
        public ICommand MultiSelectGetSelectedItemsCommand
        {
            get
            {
                return _multiSelectGetSelectedItemsCommand ??
                    (_multiSelectGetSelectedItemsCommand = new Command(() =>
                    {
                        GetMultiSelectedItems();
                    }));
            }
        }
        public ObservableCollection<MultiSelectItem> GetMultiSelectedItems()
        {
            var selected = MultiSelectItems
                .Where(p => p.IsSelected)
                .Select(p => p.Item)
                .ToList();
            return new ObservableCollection<MultiSelectItem>(selected);
        }

        public ObservableCollection<MultiSelectItem> MockData()
        {
            var items = new ObservableCollection<MultiSelectItem>();
            // Mock Data
            for (var i = 1; i < 20; i++)
            {
                items.Add(new MultiSelectItem
                {
                    Name = $"text{i}",
                    IsSelected = false
                });
            }
            return items;
        }
    }

    public class MultiSelectableCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(MultiSelectableCell), "");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        private Label lbName;

        public MultiSelectableCell()
        {
            lbName = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };

            Grid infoLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(4,GridUnitType.Star) },
                 //   new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) }
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

    public class MultiSelectableItemWrapper<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
    }

    public class MultiSelectItem
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}