using Firebase.Auth;
using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        //Fields
        public PopulateList populateList { get; private set; }
        public ObservableCollection<MilestoneGroup> ItemList { get; set; }
        public Command ItemTapped { get; set; }
        public Command ItemDelete { get; set; }
        public Command ItemEdit { get; set; }
        private Milestone _tappedItem;
        public Milestone TappedItem
        {
            get { return _tappedItem; }
            set
            {
                _tappedItem = value;
                OnPropertyChanged();
            }
        }

        public string WebAPIkey = "AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI";

        // On Property Change
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // constuctor
        public MainPageViewModel()
        {
            GetProfileInformationAndRefreshToken();
            populateList = new PopulateList("Fortnite");
            ItemList = populateList.MilestonesList;
            ItemTapped = new Command(eventItemTapped);
            ItemDelete = new Command(eventItemDelete);
            ItemEdit = new Command(eventItemEdit);
        }

        // events
        private void eventItemTapped()
        {
            if (TappedItem != null)
            {
                new NavigationService().PushModalPage(new CurrentValueModal(TappedItem, 1), false);
                TappedItem = null;
            }

        }

        private void eventItemEdit(object obj)
        {
            var item = (Milestone)obj;
            new NavigationService().PushModalPage(new CurrentValueModal(item, 1), false);
        }

        private void eventItemDelete(object obj)
        {
            var item = (Milestone)obj;
            new NavigationService().PushModalPage(new DeleteItemModal(item, populateList), false);
        }

        //Auto Login
        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var savedfirebaseauth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
            }
            catch (Exception ex)
            {
                 await App.Current.MainPage.DisplayAlert("Alert", "Oh no !  Token expired", "OK");
            }

        }

    }
}
