using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentValueModal : ContentPage
    {
        public Milestone CurrentItem { get; private set; }
        public CurrentValueModalViewModel getCurrentValueModalViewModel { get; set; }


        public CurrentValueModal(Milestone item)
        {
            InitializeComponent();
            TransitionModalIn();
            getCurrentValueModalViewModel = new CurrentValueModalViewModel(item, modalGrid);
            BindingContext = getCurrentValueModalViewModel;            
        }

        //Animation
        private async void TransitionModalIn()
        {
            await modalPancake.ScaleTo(1, 300, Easing.SpringOut);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            getCurrentValueModalViewModel.Item.ChangeCurrentValue();
        }

    }
}