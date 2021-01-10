using Milestone_Tracker.Data;
using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.HomePage;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.HomePage
{
    class DashboardPageViewModel : BindableObject
    {
        public ObservableCollection<pageList> DashboardCollection { get; set; }
        public Command ItemTapped { get; set; }

        private pageList _tappedItem;
        public pageList TappedItem
        {
            get { return _tappedItem; }
            set
            {
                _tappedItem = value;
                OnPropertyChanged();
            }
        }

        private bool _enable;  
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnPropertyChanged();
            }
        }

        public Command AddList { get; }
        public Command DeleteList { get; }
        public Command BackupList { get; }

        //Constructor
        public DashboardPageViewModel()
        {
            var FirstUsePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FirstUse.txt");
            
            if (!File.Exists(FirstUsePath))
            {
                File.WriteAllText(FirstUsePath,"true");
                FirstUse();
            }

            DashboardCollection = new ObservableCollection<pageList>();

            UpdateList();
            AddList = new Command(eventAddList);
            DeleteList = new Command(eventDeleteList);
            BackupList = new Command(eventBackupList);
            ItemTapped = new Command(eventOpenList);
            Enable = true;
        }

        private void eventBackupList(object obj)
        {
            var upload = new UploadAndDownload();
            upload.UploadTOStorage();
        }

        //methods
        public void UpdateList()
        {
            var JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "dashboard");
            JObject jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["list"];

            DashboardCollection.Clear();
            if (list != null)
            {
                foreach (var item in list)
                {
                    DashboardCollection.Add(new pageList(item["name"].ToString(), item["color"].ToString()));
                }
            }

        }

        //first Start
        public void FirstUse()
        {
            var directories = new string[] {"AppData","List_Data" };
            foreach (var directory in directories)
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directory);
                Directory.CreateDirectory(folder);
            }

            var files = new string[] {"DashBoardList"};

            foreach (var fileName in files)
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AppData");
                var filePath = Path.Combine(folder, fileName + ".json");
                var text = "{\"listNames\":[], \"list\": []}";
                JObject jsonObject = JObject.Parse(text);
                File.WriteAllText(filePath, jsonObject.ToString());
            }

            if (true)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Milestone_Tracker.Data.Fortnite.json");
                string text = "";
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                var jObject = JObject.Parse(text);

                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "List_Data");
                var filePath = Path.Combine(folder, "Fortnite.json");

                File.WriteAllText(filePath, jObject.ToString());
            }
        }

        // command events
        private async void eventAddList(object obj)
        {
            Enable = false;
           await new NavigationService().PushModalPage(new AddListModal(),false);
        }

        private async void eventOpenList(object obj)
        {
            if (TappedItem != null)
            {
                Enable = false;
                var item = (pageList)TappedItem;
                TappedItem = null;
                await new NavigationService().PushPage(new MainPage(item.PageName));
            }            
        }

        private async void eventDeleteList(object obj)
        {
            Enable = false;
           await new NavigationService().PushModalPage(new DeleteListModal(), false);
        }

    }
}
