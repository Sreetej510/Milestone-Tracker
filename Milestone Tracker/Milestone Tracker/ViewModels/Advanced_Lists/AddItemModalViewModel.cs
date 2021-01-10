using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using System;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    class AddItemModalViewModel : BindableObject
    {
        public Grid ModalGrid { get; }

        private PopulateList populateList;
        public string Name { get; set; }
        public string Category { get; set; }

        private string _checkpointString;
        public string CheckpointString
        {
            get { return _checkpointString; }
            set
            {
                _checkpointString = value;
                OnPropertyChanged();
            }
        }
        public int CurrentValue { get; set; }
        public Command CloseAddItemModal { get; }
        public Command AddItemToList { get; }
        public string ErrorMsg { get; set; }

        public AddItemModalViewModel(Grid modalGrid, PopulateList list)
        {
            ModalGrid = modalGrid;
            populateList = list;
            CloseAddItemModal = new Command(eventCloseThisModal);
            AddItemToList = new Command(eventAddItemToList);
        }

        //event
        private void eventAddItemToList()
        {
            populateList.AddItemToPopulateList(Category, Name, CheckpointString, CurrentValue);
            eventCloseThisModal();
        }

        public async void eventCloseThisModal()
        {
            await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
            new NavigationService().PopModalPage(false);
        }

    }
}
