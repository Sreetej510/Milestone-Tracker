using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel bindObject;
        public MainPage(string pageName)
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();

            bindObject = new MainPageViewModel(pageName);
            BindingContext = bindObject;
        }

        protected override void OnAppearing()
        {
            bindObject.Enable = true;
            base.OnDisappearing();
        }

    }
}
