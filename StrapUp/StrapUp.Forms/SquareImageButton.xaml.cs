using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StrapUp.Forms.Controls
{
    public partial class SquareImageButton : ExtendedFrame
    {
        public SquareImageButton()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
            {
                ExtendedFrame fr = (ExtendedFrame)AL.Parent;
                fr.OutlineColor = Color.Transparent;
            }
        }

        #region Overrides
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "Source")
            {
                xImage.Source = Source;
                
            }
          
            if (propertyName == "AnimateCardClick")
            {
                if (AnimateCardClick == true)
                {
                    var tapped = new TapGestureRecognizer();
                    tapped.Tapped += AnimateCardClickCommand;

                    AL.GestureRecognizers.Add(tapped);
                }
            }
            if (propertyName == "Click" || propertyName == "ClickParameter")
            {
                var tap = new TapGestureRecognizer();
                tap.Command = Click;
                tap.CommandParameter = ClickParameter;
                if (Click != null && ClickParameter != null)
                {
                    if (!AL.GestureRecognizers.Contains(tap))
                    {
                        AL.GestureRecognizers.Add(tap);
                    }
                }
            }
            if (propertyName == "ShadeColor")
            {
                xBoxView.Color = ShadeColor;
            }
            if (propertyName == "ShadeOpacity")
            {
                xBoxView.Opacity = ShadeOpacity;
            }



            base.OnPropertyChanged(propertyName);
        }
        #endregion

        #region Bindable Properties
        public static readonly BindableProperty StackLayoutProperty = BindableProperty.Create(
        propertyName: "Stack",
        returnType: typeof(StackLayout),
        declaringType: typeof(SquareImageButton),
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
         declaringType: typeof(SquareImageButton),
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
            declaringType: typeof(SquareImageButton),
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

        public static readonly BindableProperty ClickProperty = BindableProperty.Create(
           propertyName: "Click",
           returnType: typeof(Command),
           declaringType: typeof(SquareImageButton),
           defaultValue: null,
           defaultBindingMode: BindingMode.TwoWay);
        public Command Click
        {
            get { return (Command)GetValue(ClickProperty); }
            set
            {
                SetValue(ClickProperty, value);
            }
        }

        public static readonly BindableProperty ClickParameterProperty = BindableProperty.Create(
          propertyName: "ClickParameter",
          returnType: typeof(object),
          declaringType: typeof(SquareImageButton),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay);
        public object ClickParameter
        {
            get { return GetValue(ClickParameterProperty); }
            set
            {
                SetValue(ClickParameterProperty, value);
            }
        }

        public static readonly BindableProperty ShadeColorProperty = BindableProperty.Create(
        propertyName: "ShadeColor",
        returnType: typeof(Color),
        declaringType: typeof(SquareImageButton),
        defaultValue: Color.White,
        defaultBindingMode: BindingMode.TwoWay);
        public Color ShadeColor
        {
            get { return (Color)GetValue(ShadeColorProperty); }
            set
            {

                SetValue(ShadeColorProperty, value);
            }
        }

        public static readonly BindableProperty ShadeOpacityProperty = BindableProperty.Create(
        propertyName: "ShadeOpacity",
        returnType: typeof(double),
        declaringType: typeof(SquareImageButton),
        defaultValue: 0.0,
        defaultBindingMode: BindingMode.TwoWay);
        public double ShadeOpacity
        {
            get { return (double)GetValue(ShadeOpacityProperty); }
            set
            {

                SetValue(ShadeOpacityProperty, value);
            }
        }



        #endregion

        #region Functions
        public async void AnimateCardClickCommand(object sender, EventArgs e)
        {
            try
            {
                // animation
                var sl = (AbsoluteLayout)sender;
                var item = (Frame)sl.Parent;
                await item.ScaleTo(0.95, 50, Easing.CubicOut);
                await item.ScaleTo(1, 50, Easing.CubicIn);
            }

            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }

        }


        #endregion
    }
}