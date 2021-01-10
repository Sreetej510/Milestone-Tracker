using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels.Advanced_Lists;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemModal : ContentPage
    {
        public AddItemModal(PopulateList populateList)
        {
            InitializeComponent();
            TransitionModalIn();
            BindingContext = new AddItemModalViewModel(modalGrid,populateList);
        }

        private async void TransitionModalIn()
        {
            await modalContainer.ScaleTo(1, 300, Easing.SpringOut);
        }
    }
}