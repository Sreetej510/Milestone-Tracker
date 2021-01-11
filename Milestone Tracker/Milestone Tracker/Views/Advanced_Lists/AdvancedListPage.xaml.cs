using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    public partial class AdvancedListPage : ContentPage
    {
        private AdvancedListPageViewModel bindObject;

        public AdvancedListPage(string pageName)
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();

            bindObject = new AdvancedListPageViewModel(pageName);
            BindingContext = bindObject;
        }

        protected override void OnAppearing()
        {
            bindObject.Enable = true;
            base.OnDisappearing();
        }
    }
}