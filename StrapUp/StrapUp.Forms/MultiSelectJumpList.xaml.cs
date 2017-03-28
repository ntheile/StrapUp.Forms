using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StrapUp.Forms.Controls
{
    public partial class MultiSelectJumpList : StackLayout
    {
        public MultiSelectJumpList()
        {
            InitializeComponent();
        }
        
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
        //declaringType: typeof(MultiSelectJumpList),
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
        declaringType: typeof(MultiSelectJumpList),
        defaultBindingMode: BindingMode.TwoWay);

        public StackLayout MultiSelectJumpListHolder
        {
            get { return (StackLayout)GetValue(StackLayoutListHolderProperty); }
            set
            {
                xMultiSelectJumpListHolder.Children.Add(value);
                SetValue(StackLayoutListHolderProperty, value);
            }
        }

        public static readonly BindableProperty StackLayoutEndOfViewProperty = BindableProperty.Create(
        propertyName: "",
        returnType: typeof(StackLayout),
        declaringType: typeof(MultiSelectJumpList),
        defaultBindingMode: BindingMode.TwoWay);

        public StackLayout MultiSelectJumpListEndOfViewButtonHolder
        {
            get { return (StackLayout)GetValue(StackLayoutEndOfViewProperty); }
            set
            {
                xMultiSelectJumpListEndOfViewButtonHolder.Children.Add(value);
                SetValue(StackLayoutEndOfViewProperty, value);
            }
        }
        #endregion
    }
}