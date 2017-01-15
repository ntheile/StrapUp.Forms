using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StrapUp.Forms.Controls
{
    public partial class CardView : StackLayout
    {

        public CardView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region Bindable Properties
        public static readonly BindableProperty CardViewTextProperty = BindableProperty.Create(
          propertyName: "CardViewText",
          returnType: typeof(string),
          declaringType: typeof(CardView),
          defaultValue: "");
        public string CardViewText
        {
            get { return (string)GetValue (CardViewTextProperty); }
            set {
                SetValue(CardViewTextProperty, value);
            }
        }

        public static readonly BindableProperty CardViewDetailProperty = BindableProperty.Create(
         propertyName: "CardViewDetail",
         returnType: typeof(string),
         declaringType: typeof(CardView),
         defaultValue: "");
        public string CardViewDetail
        {
            get { return (string)GetValue(CardViewDetailProperty); }
            set
            {
                SetValue(CardViewDetailProperty, value);
            }
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
         propertyName: "Source",
         returnType: typeof(string),
         declaringType: typeof(CardView),
         defaultValue: "");
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion

        #region Functions
        public async void LinkClicked(object sender, EventArgs e)
        {
            // animation
            var item = (StackLayout)sender;
            await item.ScaleTo(0.95, 50, Easing.CubicOut);
            await item.ScaleTo(1, 50, Easing.CubicIn);

            //TODO nav to link
        }
        #endregion
    }
}
