using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;

namespace Milestone_Tracker.Views.Advanced_Lists
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
