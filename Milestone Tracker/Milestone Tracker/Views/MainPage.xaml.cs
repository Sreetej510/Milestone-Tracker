using Milestone_Tracker.ViewModels;
using Xamarin.Forms;

namespace Milestone_Tracker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

    }
}
