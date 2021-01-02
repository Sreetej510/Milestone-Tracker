using Milestone_Tracker.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Milestone_Tracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "Brush_Experimental" });

            listView.ItemsSource = new List<MilestoneGroup>
            {
                new MilestoneGroup("Elims"){
                    new Milestone("kills",new int[] {10, 20, 50, 100, 150}, 15),
                    new Milestone("kills with smg and whatever you want or try a pickaxe if you feel like it",new int[] {50, 100, 250, 500, 1000},20)
                },
                new MilestoneGroup("Collect")
                {
                    new Milestone("fish",new int[] {10, 20, 50, 100, 150},55),
                    new Milestone("wood",new int[] {50, 100, 250, 500, 1000},100)
                },
                new MilestoneGroup("Damage")
                {
                    new Milestone("burn",new int[] {50, 100, 250, 500, 1000},228),
                    new Milestone("damage to enemy stuctures",new int[] {10, 20, 50, 100, 150},120)
                }
            };
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Milestone;
            await Navigation.PushModalAsync(new CurrentValueModal(item),false);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }
    }
}
