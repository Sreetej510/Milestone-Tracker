using Milestone_Tracker.Data;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.HomePage;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
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

        public Command AddList { get; }



        //Constructor
        public DashboardPageViewModel()
        {
            DashboardCollection = new ObservableCollection<pageList>();

            UpdateList();

            AddList = new Command(eventAddList);

            

            ItemTapped = new Command(eventOpenList);
        }

        //methods
        public void UpdateList()
        {
            var JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "dashboard");
            JObject jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["list"];

            DashboardCollection.Clear();

            foreach (var item in list)
            {
                DashboardCollection.Add(new pageList(item["name"].ToString(), item["color"].ToString()));
            }

        }



        // command events
        private void eventAddList(object obj)
        {
            new NavigationService().PushModalPage(new AddListModal(),false);
        }

        private void eventOpenList(object obj)
        {
            if (TappedItem != null)
            {
                new NavigationService().PushPage(new MainPage(TappedItem.PageName));
                TappedItem = null;
            }            
        }
    }
}
