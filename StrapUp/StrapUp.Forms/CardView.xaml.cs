using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StrapUp.Forms.Controls
{
    public partial class CardView : Frame
    {

        public CardView()
        {
            InitializeComponent();

        }


		#region Overrides
		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (propertyName == "Source")
			{
				xImg.Source = Source;
			}

			if (propertyName == "AnimateCardClick")
			{
				if (AnimateCardClick == true)
				{
					var tapped = new TapGestureRecognizer();
					tapped.Tapped += AnimateCardClickCommand;

					SL.GestureRecognizers.Add(tapped);
				}
			}

			base.OnPropertyChanged(propertyName);
		}
		#endregion

		#region Bindable Properties
		//public static readonly BindableProperty CardViewTextProperty = BindableProperty.Create(
		//  propertyName: "CardViewText",
		//  returnType: typeof(string),
		//  declaringType: typeof(CardView),
		//  defaultValue: "");
		//public string CardViewText
		//{
		//    get { return (string)GetValue (CardViewTextProperty); }
		//    set {
		//        SetValue(CardViewTextProperty, value);
		//    }
		//}

		//public static readonly BindableProperty CardViewDetailProperty = BindableProperty.Create(
		// propertyName: "CardViewDetail",
		// returnType: typeof(string),
		// declaringType: typeof(CardView),
		// defaultValue: "");
		//public string CardViewDetail
		//{
		//    get { return (string)GetValue(CardViewDetailProperty); }
		//    set
		//    {
		//        SetValue(CardViewDetailProperty, value);
		//    }
		//}

		public static readonly BindableProperty StackLayoutProperty = BindableProperty.Create(
		propertyName: "Stack",
		returnType: typeof(StackLayout),
		declaringType: typeof(CardView),
		defaultBindingMode: BindingMode.TwoWay);

		public StackLayout Stack
		{
			get { return (StackLayout)GetValue(StackLayoutProperty); }
			set
			{
				xStack.Children.Add(value);
				SetValue(StackLayoutProperty, value);
			}
		}


        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
         propertyName: "Source",
         returnType: typeof(string),
         declaringType: typeof(CardView),
		 defaultBindingMode: BindingMode.TwoWay);
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set
            {
				
                SetValue(SourceProperty, value);
            }
        }



        public static readonly BindableProperty AnimateCardClickProperty = BindableProperty.Create(
			propertyName: "AnimateCardClick",
			returnType: typeof(bool),
			declaringType: typeof(CardView),
			defaultValue: false,
			defaultBindingMode: BindingMode.TwoWay);
		public bool AnimateCardClick
		{
			get { return (bool)GetValue(AnimateCardClickProperty); }
			set
			{
				SetValue(AnimateCardClickProperty, value);
			}
		}


		#endregion

		#region Functions
		public async void AnimateCardClickCommand(object sender, EventArgs e)
        {
			try
			{
				// animation
				var sl = (StackLayout)sender;
				var item = (Frame)sl.Parent;
				await item.ScaleTo(0.95, 50, Easing.CubicOut);
				await item.ScaleTo(1, 50, Easing.CubicIn);
			}
			catch (Exception ex)
			{
				
			}

        }
        #endregion
    }
}
