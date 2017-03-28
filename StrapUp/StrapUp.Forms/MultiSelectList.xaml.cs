using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StrapUp.Forms.Controls
{
    public partial class MultiSelectList : StackLayout
    {
        //public TapGestureRecognizer userTappedMethod { get; set; }
        public MultiSelectList()
        {
            InitializeComponent();
        }

        //public List<T> Collection { get; set; }

        #region Overrides
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
        #endregion

        #region Bindable Properties
        //public static readonly BindableProperty StackLayoutToolbarProperty = BindableProperty.Create(
        //propertyName: "",
        //returnType: typeof(ToolbarItem),
        //declaringType: typeof(MultiSelectList),
        //defaultBindingMode: BindingMode.TwoWay);

        //public ToolbarItem MultiSelectableToolbarHolder
        //{
        //    get { return (ToolbarItem)GetValue(StackLayoutToolbarProperty); }
        //    set
        //    {
        //        //xMultiSelectableToolbarHolder.Children.Add(value);
        //        SetValue(StackLayoutToolbarProperty, value);
        //    }
        //}

        public static readonly BindableProperty StackLayoutListHolderProperty = BindableProperty.Create(
        propertyName: "",
        returnType: typeof(StackLayout),
        declaringType: typeof(MultiSelectList),
        defaultBindingMode: BindingMode.TwoWay);

        public StackLayout MultiSelectListHolder
        {
            get { return (StackLayout)GetValue(StackLayoutListHolderProperty); }
            set
            {
                xMultiSelectListHolder.Children.Add(value);
                SetValue(StackLayoutListHolderProperty, value);
            }
        }

        public static readonly BindableProperty StackLayoutEndOfViewProperty = BindableProperty.Create(
        propertyName: "",
        returnType: typeof(StackLayout),
        declaringType: typeof(MultiSelectList),
        defaultBindingMode: BindingMode.TwoWay);

        public StackLayout MultiSelectListEndOfViewButtonHolder
        {
            get { return (StackLayout)GetValue(StackLayoutEndOfViewProperty); }
            set
            {
                xMultiSelectListEndOfViewButtonHolder.Children.Add(value);
                SetValue(StackLayoutEndOfViewProperty, value);
            }
        }
        #endregion
    }
}