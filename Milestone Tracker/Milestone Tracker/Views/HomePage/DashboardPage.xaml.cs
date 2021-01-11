using Milestone_Tracker.ViewModels.HomePage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        private readonly DashboardPageViewModel dashPageViewModal;

        public DashboardPage()
        {
            InitializeComponent();
            dashPageViewModal = new DashboardPageViewModel();
            BindingContext = dashPageViewModal;
        }

        protected override void OnAppearing()
        {
            dashPageViewModal.UpdateList();
            dashPageViewModal.Enable = true;
            base.OnAppearing();
        }
    }
}