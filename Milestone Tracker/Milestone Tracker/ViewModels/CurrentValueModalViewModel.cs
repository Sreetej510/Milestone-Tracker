using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Milestone_Tracker.ViewModels
{
    public class CurrentValueModalViewModel
    {
        public Milestone Item { get; set; }

        public Command CloseCurrentValueModal { get; set; }
        public Command SliderValueChange { get; set; }

        public Grid ModalGrid { get; set; }
        public PancakeView ModalPancake { get; set; }


        // Constructor
        public CurrentValueModalViewModel(Milestone item, Grid modalGrid)
        {
            Item = item;
            ModalGrid = modalGrid;
            CloseCurrentValueModal = new Command(eventCloseCurrentValueModal);
            SliderValueChange = new Command(eventSliderValueChange);
        }

        // events
        private async void eventCloseCurrentValueModal(object obj)
        {
            await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
            new NavigationService().PopModalPage(false);
        }

        private void eventSliderValueChange(object obj)
        {
            Item.ChangeCurrentValue();
        }
    }
}
