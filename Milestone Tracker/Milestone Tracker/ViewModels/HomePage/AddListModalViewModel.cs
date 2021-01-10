using Milestone_Tracker.Data;
using Milestone_Tracker.Navigation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.HomePage
{
    public class AddListModalViewModel : BindableObject
    {
        public Grid ModalGrid { get; }
        public Command CloseAddListModal { get; }
        public Command AddToList { get; }
        public bool DoneEnable
        {
            get
            {
                if (TaskName != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value;
                OnPropertyChanged(); 
            }
        }

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoneEnable));
            }
        }

        public bool IsAdvanced { get; set; }

        private static string[] colors = new string[] { "#F72585", "#7209B7", "#3A0CA3", "#4361EE", "#4CC9F0", "#fee440", "#9b5de5", "#f15bb5", "00bbf9", "#00f5d4" };

        public AddListModalViewModel(Grid modalGrid)
        {
            ModalGrid = modalGrid;
            CloseAddListModal = new Command(eventCloseThisModal);
            AddToList = new Command(eventAddToList);
        }


        private void eventAddToList(object obj)
        {
            // change to normal

            var type = "advanced";
            // the above variable needed to be changed

            if (IsAdvanced)
            {
                type = "advanced";
            }

            var rnd = new Random();
            var pageColor = colors[rnd.Next(9)]; 

            var JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", type);
            JObject jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["list"];
            var listNames = (JArray)jObject["listNames"];

            List<string> strings = listNames.ToObject<List<string>>();

            if (strings.Contains(TaskName.Trim().ToUpper()))
            {
                ErrorMsg = "Task with same name already exists";
            }
            else
            {
                var jsonString = " {\"name\": \"" + TaskName + "\", \"Type\":\"" + type + "\", \"color\":\"" + pageColor + "\"}";

                var jsonObject = JObject.Parse(jsonString);

                list.Add(jsonObject);
                listNames.Add(TaskName.Trim().ToUpper());

                JsonFIleActivities.WriteJson(jObject);

                new NavigationService().PopModalPage(false);
            }

            
        }

        public async void eventCloseThisModal()
        {
            await ModalGrid.FadeTo(0, 100, Easing.CubicIn);
            new NavigationService().PopModalPage(false);
        }
    }
}
