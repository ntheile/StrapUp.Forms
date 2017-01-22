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
    public partial class ExpansionPanel : StackLayout
    {
        public bool IsVis { get; set; }
        public string ArrowOrientation { get; set; }
        public TapGestureRecognizer userTappedMethod { get; set; }

        public ExpansionPanel()
        {
            IsVis = false;
            InitializeComponent();
            Arrow.Text = "fa-chevron-down";

            if (userTappedMethod == null)
            {
                userTappedMethod = new TapGestureRecognizer();
                userTappedMethod.Tapped += (sender, eventArgs) =>
                {
                    //Execute the passed In Method from the user Command Binding
                    var expansionPanelControl = this;
                    Object expansionPanelEventArg = new { sender, eventArgs, expansionPanelControl };
                    Execute(this.Command, expansionPanelEventArg);
                    ToggleVisible(sender, eventArgs);
                };
                xCollapsedPanelWrapper.GestureRecognizers.Add(userTappedMethod);
            }
        }


        #region Overrides
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "HideIcon")
            {
                if (HideIcon == true)
                {
                    xIcon.IsVisible = false;
                }
            }
            base.OnPropertyChanged(propertyName);
        }
        #endregion


        #region Events
        /// <summary>
        /// This is overrideable so you can have your own toggle logic
        /// espeically for iOS you will have to force layout reday on the viewcell,
        /// wherever it lives up the .Parent tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual async void ToggleVisible(object sender, EventArgs e)
        {
            var me = sender as StackLayout;   
            if (xExpandedPanel.IsVisible == false) // show
            {
                Arrow.Text = "fa-chevron-up";
                xExpandedPanel.IsVisible = true;
                //xExpandedPanel.ForceLayout();

               
                Device.OnPlatform(iOS: () =>
                {
                    RedrawIOS(me);
                });

            }
            else //hide
            {
                Arrow.Text = "fa-chevron-down";
                xExpandedPanel.IsVisible = false;
                //xExpandedPanel.ForceLayout();

                Device.OnPlatform(iOS: () =>
                {
                    RedrawIOS(me);
                });
            }
        }
        #endregion


        #region Bindable Properties
        public static readonly BindableProperty StackLayoutProperty = BindableProperty.Create(
        propertyName: "",
        returnType: typeof(StackLayout),
        declaringType: typeof(ExpansionPanel),
        defaultBindingMode: BindingMode.TwoWay);

        public StackLayout ExpandedPanel
        {
            get { return (StackLayout)GetValue(StackLayoutProperty); }
            set
            {
                xExpandedPanel.Children.Add(value);
                SetValue(StackLayoutProperty, value);
            }
        }

        public StackLayout CollapsedPanel
        {
            get { return (StackLayout)GetValue(StackLayoutProperty); }
            set
            {
                xCollapsedPanel.Children.Add(value);
                SetValue(StackLayoutProperty, value);
            }
        }


        public static readonly BindableProperty HideIconProperty = BindableProperty.Create(
            propertyName: "HideIcon",
            returnType: typeof(bool),
            declaringType: typeof(ExpansionPanel),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);
        public bool HideIcon
        {
            get { return (bool)GetValue(HideIconProperty); }
            set
            {
                SetValue(HideIconProperty, value);
            }
        }


        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command), 
                typeof(ICommand), 
                typeof(ExpansionPanel), 
                null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        
        public static readonly BindableProperty ParentViewCellLevelProperty = BindableProperty.Create(
           propertyName: "ParentViewCellLevel",
           returnType: typeof(int),
           declaringType: typeof(ExpansionPanel),
           defaultValue: 2,
           defaultBindingMode: BindingMode.TwoWay);
        public int ParentViewCellLevel
        {
            get { return (int)GetValue(ParentViewCellLevelProperty); }
            set
            {
                SetValue(ParentViewCellLevelProperty, value);
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
        // Helper method for invoking commands safely
        public static void Execute(ICommand command, object param)
        {
            if (command == null) return;
            if (command.CanExecute(param))
            {
                command.Execute(param);
            }
            else if (command.CanExecute(null))
            {
                command.Execute(null);
            }
            else
            {
                return;
            }
        }
        public virtual void RedrawIOS(StackLayout stackLayoutWrapper)
        {
            ViewCell viewCell = null;
            if (ParentViewCellLevel == 3)
            {
                viewCell = stackLayoutWrapper.Parent.Parent.Parent as ViewCell;
            }

            if (ParentViewCellLevel == 2)
            {
                viewCell = stackLayoutWrapper.Parent.Parent as ViewCell;
            }

            if (viewCell != null)
            {
                viewCell.ForceUpdateSize();
            }
        }
        #endregion
    }
}
