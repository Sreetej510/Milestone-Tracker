using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentValueModal : ContentPage
    {
        private CurrentValueModalViewModel _bindingContext;
        public CurrentValueModal(Milestone item, int count)
        {
            InitializeComponent();
            _bindingContext = new CurrentValueModalViewModel(item, modalGrid, modalContainer, count);
            BindingContext = _bindingContext;
            TransitionModalIn();
        }

        //Animation
        private async void TransitionModalIn()
        {
            await modalContainer.TranslateTo(0, 0, 300);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _bindingContext.RingProgress = (float)_bindingContext.SliderValue / (float)_bindingContext.Item.CurrentEndValue;
            if (_bindingContext.RingProgress == 1)
            {
                _bindingContext.ButtonText = "Next Stage";

                if (_bindingContext.Item.CurrentCheckpoint == _bindingContext.Item.NumOfCheckpoints)
                {
                    _bindingContext.ButtonText = "Finished";
                }
            }
            else
            {
                _bindingContext.ButtonText = "Update";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}