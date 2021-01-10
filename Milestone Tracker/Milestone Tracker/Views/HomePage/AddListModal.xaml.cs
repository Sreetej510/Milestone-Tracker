using Milestone_Tracker.ViewModels.HomePage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddListModal : ContentPage
    {
        private AddListModalViewModel _bindingContext;

        public AddListModal()
        {
            InitializeComponent();
            TransitionModalIn();
            _bindingContext = new AddListModalViewModel(modalGrid);            
            BindingContext = _bindingContext;            
        }


        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1,300,Easing.SpringOut);
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.eventCloseThisModal();
            return true;
        }

    }
}