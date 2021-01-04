using FreshMvvm;
using Milestone_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Milestone_Tracker.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        public Command ItemTapped { get; set; }
        private Models.Milestone _tappedItem;
        public PopulateList ItemsList { get; set; }

        public ObservableCollection<MilestoneGroup> MileStoneCollection { get; set; }

        public Models.Milestone TappedItem
        {
            get { return _tappedItem; }
            set 
            {
                _tappedItem = value;
            }
        }

        public MainPageModel()
        {
            ItemsList = new PopulateList("Fortnite");
            MileStoneCollection = ItemsList.MilestonesList;
            ItemTapped = new Command(ListItemTapped);

        }

        private void ListItemTapped()
        {
            CoreMethods.PushPageModel<CurrentValuePageModel>(_tappedItem);
        }


    }
}
