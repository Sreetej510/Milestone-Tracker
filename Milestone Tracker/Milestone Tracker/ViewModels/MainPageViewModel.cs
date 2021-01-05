using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels
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


        // On Property Change
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // constuctor
        public MainPageViewModel()
        {
            populateList = new PopulateList("Forntite");
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
                new NavigationService().PushModalPage(new CurrentValueModal(TappedItem), false);
                TappedItem = null;
            }

        }

        private void eventItemEdit(object obj)
        {
            var item = (Milestone)obj;
            new NavigationService().PushModalPage(new CurrentValueModal(item), false);
        }

        private void eventItemDelete(object obj)
        {
            var item = (Milestone)obj;
            new NavigationService().PushModalPage(new DeleteItemModal(item, populateList), false);
        }

    }
}
