using Milestone_Tracker.Data;
using Milestone_Tracker.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Milestone_Tracker.Models
{
    public class PopulateList : BindableObject
    {
        private ObservableCollection<MilestoneGroup> _milestonesList;
        private ReadAndWriteJson JsonFIleActivities;

        public ObservableCollection<MilestoneGroup> MilestonesList
        {
            get
            {
                return _milestonesList;
            }
            set
            {
                _milestonesList = value;
                OnPropertyChanged();
            }
        }

        public PopulateList(string listname)
        {
            MilestonesList = new ObservableCollection<MilestoneGroup>();
            JsonFIleActivities = new ReadAndWriteJson(listname);

            var jObject  = JsonFIleActivities.ReadJson();

            var onGoing = jObject["onGoing"];

            var list = (JArray)onGoing;

            
            foreach (var group in list)
            {
                string category = group["category"].ToString();
                
                var groupIndex = list.IndexOf(group);

                MilestonesList.Add(new MilestoneGroup(category, groupIndex));

                var itemList = group["contents"];

                foreach (var item  in (JArray)itemList)
                {
                    string name = item["name"].ToString();

                    JArray jArray = (JArray)item["checkpoints"];
                    var checkpoints = jArray.ToObject<int[]>();

                    int currentValue = (int)item["currentValue"];
                    MilestonesList[groupIndex].Add(new Milestone(groupIndex, name, checkpoints, currentValue));
                }   

            };

        }


    }
}