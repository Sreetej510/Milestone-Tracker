using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels;

using Xamarin.Forms;

namespace Milestone_Tracker.Views
{
    public partial class DeleteItemModal : ContentPage
    {
        public DeleteItemModal(Milestone item, PopulateList populateList)
        {
            BindingContext = new DeleteItemModalViewModel(item, populateList);
            InitializeComponent();
            TransitionModalIn();
        }

        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1, 300, Easing.SpringOut);
        }

    }
}