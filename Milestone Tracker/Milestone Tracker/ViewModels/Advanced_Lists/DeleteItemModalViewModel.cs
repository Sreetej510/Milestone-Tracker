using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Xamarin.Forms;

namespace Milestone_Tracker.Views.Advanced_Lists
{
    internal class DeleteItemModalViewModel
    {
        public string ListName { get; }
        public Milestone DeleteItem { get; set; }
        public Command DeleteOk { get; set; }
        public PopulateList NewList { get; set; }
        public Command DeleteCancel { get; set; }

        public DeleteItemModalViewModel(string listName, Milestone item, PopulateList populateList)
        {
            ListName = listName;
            DeleteItem = item;
            NewList = populateList;
            DeleteOk = new Command(EventDeleteItem);
            DeleteCancel = new Command(EventCancelDelete);
        }

        private void EventCancelDelete(object obj)
        {
            new NavigationService().PopModalPage(false);
        }

        private void EventDeleteItem(object obj)
        {
            var populateList = (PopulateList)obj;

            populateList.DeletItemFromPopulateList(DeleteItem);

            new NavigationService().PopModalPage(false);
        }
    }
}