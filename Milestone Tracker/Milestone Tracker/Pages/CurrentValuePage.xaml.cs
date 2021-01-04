using System;
using Milestone_Tracker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentValuePage : ContentPage
    {
        public Models.Milestone Item { get; private set; }

        public CurrentValuePage()
        {
            InitializeComponent();
        }

        private async void TappedOutside(object sender, EventArgs e)
        {
            //await modalGrid.FadeTo(0, 100, Easing.CubicIn);
            //await Navigation.PopModalAsync(false);
        }

        //Animation
        private async void TransitionModalIn()
        {
           // await modalPancake.ScaleTo(1, 300, Easing.SpringOut);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Item.ChangeCurrentValue((int)e.NewValue);
        }
    }
}