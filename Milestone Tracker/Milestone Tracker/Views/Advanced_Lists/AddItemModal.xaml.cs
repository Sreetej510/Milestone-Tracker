using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemModal : ContentPage
    {
        private AddItemModalViewModel _bindingObject;
        public AddItemModal(PopulateList populateList)
        {
            InitializeComponent();
            TransitionModalIn();

            _bindingObject = new AddItemModalViewModel(modalGrid,populateList);
            BindingContext = _bindingObject;
        }

        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1, 300, Easing.SpringOut);
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingObject.eventCloseThisModal();
            return true;
        }

    }
}