using Firebase.Auth;
using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views;
using Milestone_Tracker.Views.Advanced_Lists;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        //Fields
        public string ListName { get; }
        public PopulateList populateList { get; private set; }
        public ObservableCollection<MilestoneGroup> ItemList { get; set; }
        public ObservableCollection<MilestoneGroup> SearchItemList { get; set; }
        public Command ItemTapped { get; set; }
        public Command ItemDelete { get; set; }
        public Command ItemAdd { get; set; }

        private bool _enable;

        public bool Enable
        {
            get { return _enable; }
            set { _enable = value;
                OnPropertyChanged();
            }
        }

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

        public string SearchBarText { get; private set; }

        public string WebAPIkey = "AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI";

        // On Property Change
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // constuctor
        public MainPageViewModel(string listName)
        {
            //GetProfileInformationAndRefreshToken();
            ListName = listName;
            populateList = new PopulateList(listName);
            ItemList = populateList.MilestonesList;
            ItemTapped = new Command(eventItemTapped);
            ItemDelete = new Command(eventItemDelete);
            ItemEdit = new Command(eventItemEdit);
            ItemAdd = new Command(eventItemAdd);
            Enable = true;
        }

        // events
        private async void eventItemAdd(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new AddItemModal(populateList), false);
        }

        private async void eventItemTapped()
        {

            var item = (Milestone)TappedItem;
            TappedItem = null;
            if (item != null && Enable)
            {
                Enable = false;
                await new NavigationService().PushModalPage(new CurrentValueModal(item, 1), false);
            }
        }

        private async void eventItemEdit(object obj)
        {
            var item = (Milestone)obj;
            await new NavigationService().PushModalPage(new CurrentValueModal(item, 1), false);
        }

        private async void eventItemDelete(object obj)
        {
            var item = (Milestone)obj;
            await new NavigationService().PushModalPage(new DeleteItemModal(ListName ,item, populateList), false);
        }

        //Auto Login
        //async private void GetProfileInformationAndRefreshToken()
        //{
        //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
        //    try
        //    {
        //        var savedfirebaseauth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
        //        var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
        //        Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
        //    }
        //    catch (Exception ex)
        //    {
        //         await App.Current.MainPage.DisplayAlert("Alert", "Oh no !  Token expired", "OK");
        //    }

        //}

        
    }
}