using Milestone_Tracker.ViewModels.HomePage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuModal : ContentPage
    {
        private readonly MenuModalViewModel _bindingContext;

        public MenuModal()
        {
            InitializeComponent();
            _bindingContext = new MenuModalViewModel(modalGrid, modalContainer);
            BindingContext = _bindingContext;
            Transition();
        }

        private void Transition()
        {
            modalContainer.TranslateTo(0, 0, 400);
        }

        protected override void OnAppearing()
        {
            _bindingContext.LoginStatus();
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.EventCloseThisModal();
            return true;
        }
    }
}