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
    internal class DashboardPageViewModel : BindableObject
    {
        public ObservableCollection<PageList> DashboardCollection { get; set; }
        public Command ItemTapped { get; set; }

        private PageList _tappedItem;

        public PageList TappedItem
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
                File.WriteAllText(FirstUsePath, "true");
                FirstUse();
            }

            DashboardCollection = new ObservableCollection<PageList>();

            UpdateList();
            AddList = new Command(EventAddList);
            DeleteList = new Command(EventDeleteList);
            BackupList = new Command(EventBackupList);
            ItemTapped = new Command(EventOpenList);
            Enable = true;
        }

        private void EventBackupList(object obj)
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
                    DashboardCollection.Add(new PageList(item["name"].ToString(), item["color"].ToString()));
                }
            }
        }

        //first Start
        public void FirstUse()
        {
            var directories = new string[] { "AppData", "List_Data" };
            foreach (var directory in directories)
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directory);
                Directory.CreateDirectory(folder);
            }

            var files = new string[] { "DashBoardList" };

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
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AdvancedListPage)).Assembly;
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
        private async void EventAddList(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new AddListModal(), false);
        }

        private async void EventOpenList(object obj)
        {
            if (TappedItem != null)
            {
                Enable = false;
                var item = (PageList)TappedItem;
                TappedItem = null;
                await new NavigationService().PushPage(new AdvancedListPage(item.PageName));
            }
        }

        private async void EventDeleteList(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new DeleteListModal(), false);
        }
    }
}