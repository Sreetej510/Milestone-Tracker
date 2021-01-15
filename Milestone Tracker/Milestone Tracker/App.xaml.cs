using Milestone_Tracker.Data;
using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.HomePage;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Milestone_Tracker
{
    public partial class App : Application
    {
        public ReadAndWriteJson JsonFIleActivities { get; set; }

        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "SwipeView_Experimental" });
            MainPage = new NavigationPage(new DashboardPage());
        }

        protected override void OnStart()
        {
            JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "dashboard");
            Task.Run(async () =>
            {
                var FirstUsePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FirstUse.txt");

                if (!File.Exists(FirstUsePath))
                {
                    File.WriteAllText(FirstUsePath, "true");
                    Preferences.Set("LogStatus", "loggedOut");
                    await FirstUse();
                }
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        //first Start
        public async Task FirstUse()
        {
            var directories = new string[] { "AppData", "List_Data" };
            await Task.Run(() =>
            {
                foreach (var directory in directories)
                {
                    var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directory);
                    Directory.CreateDirectory(folder);
                }
            });
            await Task.Run(() =>
            {
                var files = new string[] { "DashBoardList" };

                foreach (var fileName in files)
                {
                    var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AppData");
                    var filePath = Path.Combine(folder, fileName + ".json");
                    var text = "{\"listNames\":[], \"list\": []}";
                    JObject jsonObject = JObject.Parse(text);
                    JsonFIleActivities.WriteJson(jsonObject);
                }
            });
        }
    }
}