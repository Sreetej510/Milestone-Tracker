using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views;
using Milestone_Tracker.Views.Advanced_Lists;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    internal class AdvancedListPageViewModel : BindableObject
    {
        //Fields
        public string ListName { get; set; }

        private PopulateList _populateList;

        public PopulateList PopulateList
        {
            get { return _populateList; }
            set
            {
                _populateList = value;
                OnPropertyChanged();
            }
        }

        public Command ItemTapped { get; set; }
        public Command ItemDelete { get; set; }
        public Command ItemAdd { get; set; }

        private bool _enable;

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnPropertyChanged();
            }
        }

        public bool EnableTapped { get; private set; }
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

        // constuctor
        public AdvancedListPageViewModel(string listName)
        {
            //GetProfileInformationAndRefreshToken();

            Task.Run(() =>
            {
                Enable = true;
                EnableTapped = true;
                ItemTapped = new Command(EventItemTapped);
                ItemDelete = new Command(EventItemDelete);
                ItemEdit = new Command(EventItemEdit);
                ItemAdd = new Command(EventItemAdd);
            });
            Task.Run(() =>
            {
                PopulateList = new PopulateList(listName);
            });
            ListName = listName;
        }

        // events
        private async void EventItemAdd(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new AddItemModal(PopulateList), false);
            await Task.Delay(100);
            Enable = true;
        }

        private async void EventItemTapped()
        {
            var item = (Milestone)TappedItem;
            TappedItem = null;
            if (item != null && EnableTapped)
            {
                EnableTapped = false;
                await new NavigationService().PushModalPage(new CurrentValueModal(item, 1), false);
                EnableTapped = true;
            }
        }

        private async void EventItemEdit(object obj)
        {
            var item = (Milestone)obj;
            await new NavigationService().PushModalPage(new CurrentValueModal(item, 1), false);
        }

        private async void EventItemDelete(object obj)
        {
            var item = (Milestone)obj;
            await new NavigationService().PushModalPage(new DeleteItemModal(ListName, item, PopulateList), false);
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