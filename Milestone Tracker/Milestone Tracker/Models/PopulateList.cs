using Milestone_Tracker.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.Models
{
    public class PopulateList : BindableObject
    {
        private ObservableCollection<MilestoneGroup> _milestonesList;

        private readonly ReadAndWriteJson JsonFIleActivities;

        public string ListName { get; }

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

        public PopulateList(string listName)
        {
            ListName = listName;
            MilestonesList = new ObservableCollection<MilestoneGroup>();
            JsonFIleActivities = new ReadAndWriteJson(listName, "List_Data", "list");
            Task.Run(() =>
            {
                var jObject = JsonFIleActivities.ReadJson();

                var list = (JArray)jObject["onGoing"];
                foreach (var group in list)
                {
                    string category = group["category"].ToString();

                    MilestonesList.Add(new MilestoneGroup(category));

                    var groupIndex = MilestonesList.Count;

                    var itemList = group["contents"];

                    foreach (var item in (JArray)itemList)
                    {
                        string name = item["name"].ToString();

                        JArray jArray = (JArray)item["checkpoints"];
                        var checkpoints = jArray.ToObject<int[]>();

                        int currentValue = (int)item["currentValue"];
                        MilestonesList[groupIndex - 1].Add(new Milestone(listName, category, name, checkpoints, currentValue));
                    }
                }
            });
        }

        public void AddItemToPopulateList(string categoryInput, string name, string checkpointString, int currentValue)
        {
            var ItemJsonString = " {\"name\" : \"" + name + "\", \"checkpoints\" : [" + checkpointString.Replace('-', ',') + "], \"currentValue\" :" + currentValue + "}";

            var ItemJsonObject = JObject.Parse(ItemJsonString);

            var array = checkpointString.Split('-');
            var checkpoints = Array.ConvertAll(array, s => int.Parse(s));
            Array.Sort(checkpoints);
            var jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["onGoing"];
            var allCategories = (JArray)jObject["allCategories"];
            List<string> strings = allCategories.ToObject<List<string>>();

            var catInputCap = categoryInput.Trim().ToUpper();

            if (strings.Contains(catInputCap))
            {
                var groupIndex = strings.IndexOf(catInputCap);
                var group = list[groupIndex];
                var groupArray = (JArray)group["contents"];
                groupArray.Add(ItemJsonObject);
                MilestonesList[groupIndex].Add(new Milestone(ListName, categoryInput, name, checkpoints, currentValue));
                return;
            }
            else
            {
                allCategories.Add(catInputCap);
                var groupJsonString = "{\"category\": \"" + categoryInput + "\" ,\"contents\": [" + ItemJsonString + "]}";
                var groupJsonObject = JObject.Parse(groupJsonString);
                list.Add(groupJsonObject);
                MilestonesList.Add(new MilestoneGroup(categoryInput) {
                        new Milestone(ListName,categoryInput, name, checkpoints, currentValue)
                        });
            }

            JsonFIleActivities.WriteJson(jObject);
        }

        public void DeletItemFromPopulateList(Milestone deleteItem)
        {
            var jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["onGoing"];
            var allCategories = (JArray)jObject["allCategories"];
            List<string> strings = allCategories.ToObject<List<string>>();

            var deleteItemCatCap = deleteItem.Category.Trim().ToUpper();

            var groupIndex = strings.IndexOf(deleteItemCatCap);

            var group = list[groupIndex];

            var groupArray = (JArray)group["contents"];

            foreach (var item in groupArray)
            {
                if (item["name"].ToString() == deleteItem.Name)
                {
                    groupArray.Remove(item);

                    break;
                }
            }

            MilestonesList[groupIndex].Remove(deleteItem);

            if (groupArray.Count == 0)
            {
                list.Remove(group);
                allCategories.RemoveAt(groupIndex);
                MilestonesList.RemoveAt(groupIndex);
            }

            JsonFIleActivities.WriteJson(jObject);
        }

        public void EditItemOfPopulateList(Milestone editItem, string name, string checkpointString, int currentValue)
        {
            var jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["onGoing"];
            var allCategories = (JArray)jObject["allCategories"];
            List<string> strings = allCategories.ToObject<List<string>>();

            var deleteItemCatCap = editItem.Category.Trim().ToUpper();

            var groupIndex = strings.IndexOf(deleteItemCatCap);

            var group = list[groupIndex];

            var groupArray = (JArray)group["contents"];

            foreach (var item in groupArray)
            {
                if (item["name"].ToString() == editItem.Name)
                {
                    item["name"] = name;
                    item["checkpoints"] = checkpointString.Replace('-', ',');
                    item["currentValue"] = currentValue;
                    break;
                }
            }

            var itemIndex = MilestonesList[groupIndex].IndexOf(editItem);
            MilestonesList[groupIndex].Remove(editItem);

            var array = checkpointString.Split('-');
            var checkpoints = Array.ConvertAll(array, s => int.Parse(s));
            Array.Sort(checkpoints);

            editItem.ChageInfo(name, checkpoints, currentValue);

            MilestonesList[groupIndex].Insert(itemIndex, editItem);

            JsonFIleActivities.WriteJson(jObject);
        }
    }
}