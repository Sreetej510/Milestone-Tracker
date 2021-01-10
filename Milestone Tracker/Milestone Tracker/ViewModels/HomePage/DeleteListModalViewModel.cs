using Milestone_Tracker.Data;
using Milestone_Tracker.Navigation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.HomePage
{
    class DeleteListModalViewModel : BindableObject
    {
        public Grid ModalGrid { get; }
        public Command CloseDeleteListModal { get; }
        public Command DeleteFromList { get; }
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
            set
            {
                _errorMsg = value;
                OnPropertyChanged();
            }
        }
        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoneEnable));
            }
        }


        // Constructor
        public DeleteListModalViewModel(Grid modalGrid)
        {
            ModalGrid = modalGrid;
            CloseDeleteListModal = new Command(eventCloseThisModal);
            DeleteFromList = new Command(eventDeleteFromList);
        }


        // Events
        private void eventDeleteFromList(object obj)
        {
            var JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "advanced");
            JObject jObject = JsonFIleActivities.ReadJson();

            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "List_Data");
            var filePath = Path.Combine(folder, TaskName + ".json");

            File.Delete(filePath);

            var list = (JArray)jObject["list"];
            var listNames = (JArray)jObject["listNames"];

            List<string> strings = listNames.ToObject<List<string>>();

            if (!strings.Contains(TaskName.Trim().ToUpper()))
            {
                ErrorMsg = "Task with this name doesnot exists";
            }
            else
            {
                var v = strings.IndexOf(TaskName.Trim().ToUpper());
                list.RemoveAt(v);
                listNames.RemoveAt(v);

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
