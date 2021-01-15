using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using System;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Advanced_Lists
{
    internal class EditItemModalViewModel : BindableObject
    {
        public Grid ModalGrid { get; }
        private readonly PopulateList populateList;
        private readonly Milestone EditItem;
        public string Name { get; set; }
        public string CheckpointString { get; set; }
        public int CurrentValue { get; set; }
        private bool closeEnabled;
        public Command CloseEditItemModal { get; }
        public Command EditItemOfList { get; }
        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                OnPropertyChanged();
            }
        }

        public string EditTask { get; }
        public string Category { get; }

        //Constructor
        public EditItemModalViewModel(Grid modalGrid, PopulateList list, Milestone editItem)
        {
            EditItem = editItem;
            ModalGrid = modalGrid;
            populateList = list;
            closeEnabled = true;
            Category = "Category : " + editItem.Category;
            CloseEditItemModal = new Command(EventCloseThisModal);
            EditItemOfList = new Command(EventEditItemOfList);
            EditTask = "Edit : " + editItem.Name;
            CurrentValue = -1;
        }

        //event
        private void EventEditItemOfList()
        {
            if (Name == null)
            {
                Name = EditItem.Name;
            }
            if (CheckpointString == null)
            {
                CheckpointString = string.Join("-", EditItem.CheckpointValues);
            }
            if (CurrentValue == -1)
            {
                CurrentValue = EditItem.CurrentValue;
            }

            try
            {
                populateList.EditItemOfPopulateList(EditItem, Name, CheckpointString, CurrentValue);
                EventCloseThisModal();
            }
            catch (Exception)
            {
                ErrorMsg = "Checkpoints should only contain numbers and '-'";
            }
        }

        public async void EventCloseThisModal()
        {
            if (closeEnabled)
            {
                closeEnabled = false;
                await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
                new NavigationService().PopModalPage(false);
            }
        }
    }
}