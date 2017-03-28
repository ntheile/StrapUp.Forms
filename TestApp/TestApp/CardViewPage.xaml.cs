using StrapUp.Forms.Controls;

using Xamarin.Forms;
namespace TestApp
{
    public partial class CardViewPage : ContentPage
    {
        public CardViewPage()
        {
            InitializeComponent();

			CardView cv = new CardView();

			cv.Source = "";
        }
    }
}