using Milestone_Tracker.Models;
using Milestone_Tracker.Views.Advanced_Lists;
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