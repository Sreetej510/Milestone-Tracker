using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentValueModal : ContentPage
    {
        public Models.Milestone Item { get; set; }
        public CurrentValueModal(Models.Milestone item)
        {
            BindingContext = item;
            Item = item;
            InitializeComponent();
            TransitionModalIn();
        }

        private async void TappedOutside(object sender, EventArgs e)
        {
            await modalGrid.FadeTo(0, 100, Easing.CubicIn);
            await Navigation.PopModalAsync(false);
        }

        //Animation
        private async void TransitionModalIn()
        {
            await modalPancake.ScaleTo(1, 300, Easing.SpringOut);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Item.ChangeCurrentValue((int)e.NewValue);
        }
    }
}