using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels
{
    class DeleteItemModalViewModel
    {
        public Milestone DeleteItem { get; set; }
        public Command DeleteOk { get; set; }
        public PopulateList NewList { get; set; }
        public Command DeleteCancel { get; set; }

        public DeleteItemModalViewModel(Milestone item, PopulateList populateList)
        {
            DeleteItem = item;
            NewList = populateList;
            DeleteOk = new Command(eventDeleteItem);
            DeleteCancel = new Command(eventCancelDelete);
        }

        private void eventCancelDelete(object obj)
        {
            new NavigationService().PopModalPage(false);
        }

        private void eventDeleteItem(object obj)
        {
            var item = DeleteItem;
            var v = (PopulateList)obj;
            

            foreach(MilestoneGroup group in v.MilestonesList)
            {
                foreach (var i in group)
                {
                    if (i.Name == item.Name)
                    {
                        group.Remove(item);
                        break;
                    }
                }
            }

            new NavigationService().PopModalPage(false);

        }


    }
}
