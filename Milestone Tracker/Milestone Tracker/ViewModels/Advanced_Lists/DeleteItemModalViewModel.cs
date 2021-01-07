﻿using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Xamarin.Forms;

namespace Milestone_Tracker.Views.Advanced_Lists
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
            var populateList = (PopulateList)obj;

            foreach (var group in populateList.MilestonesList)
            {
                if (group.GroupNumber == item.GroupIndex)
                {
                    group.Remove(item);
                    if (group.Count == 0)
                    {
                        populateList.MilestonesList.Remove(group);
                    }
                    break;
                }
            }

            new NavigationService().PopModalPage(false);

        }


    }
}