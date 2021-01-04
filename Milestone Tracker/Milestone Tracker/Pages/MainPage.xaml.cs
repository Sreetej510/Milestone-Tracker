using Milestone_Tracker.Models;
using Milestone_Tracker.PageModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Milestone_Tracker.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

       /* private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Milestone;
            //await Navigation.PushModalAsync(new CurrentValuePage(), false);
        }


        private async void SwipeItem_Edit(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as Milestone;
            //await Navigation.PushModalAsync(new CurrentValuePage(item), false);
        }

        private void SwipeItem_Delete(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as Milestone;
            DisplayActionSheet(item.Name, "Cancel", "Delete");
        }


        private async void ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as Milestone;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new CurrentValueModal(item), false);

            ((CollectionView)sender).SelectedItem = null;
        }*/
    }
}
