using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditItemModal : ContentPage
    {
        private readonly EditItemModalViewModel _bindingContext;

        public EditItemModal(PopulateList populateList, Milestone editItem)
        {
            InitializeComponent();
            TransitionModalIn();
            _bindingContext = new EditItemModalViewModel(modalGrid, populateList, editItem);
            BindingContext = _bindingContext;
        }

        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1, 300, Easing.SpringOut);
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.EventCloseThisModal();
            return true;
        }
    }
}