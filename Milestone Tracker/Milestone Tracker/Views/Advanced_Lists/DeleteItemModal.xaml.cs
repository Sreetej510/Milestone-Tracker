using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Advanced_Lists;
using Xamarin.Forms;

namespace Milestone_Tracker.Views
{
    public partial class DeleteItemModal : ContentPage
    {
        public DeleteItemModal(string listName, Milestone item, PopulateList populateList)
        {
            BindingContext = new DeleteItemModalViewModel(listName, item, populateList);
            InitializeComponent();
            TransitionModalIn();
        }

        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1, 300, Easing.SpringOut);
        }

        protected override bool OnBackButtonPressed()
        {
            new NavigationService().PopModalPage(false);
            return true;
        }
    }
}