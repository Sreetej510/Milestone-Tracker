using Milestone_Tracker.Data;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.HomePage;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.HomePage
{
    internal class DashboardPageViewModel : BindableObject
    {
        public ObservableCollection<PageList> DashboardCollection { get; set; }
        public Command ItemTapped { get; set; }
        public Command MenuButtonTapped { get; private set; }

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

        public Command AddList { get; set; }
        public Command DeleteList { get; set; }
        public ReadAndWriteJson JsonFIleActivities { get; }
        public bool EnableTapped { get; private set; }

        //Constructor
        public DashboardPageViewModel()
        {
            Task.Run(() =>
            {
                DashboardCollection = new ObservableCollection<PageList>();
                UpdateList();
            });

            Enable = true;
            EnableTapped = true;

            AddList = new Command(EventAddList);
            DeleteList = new Command(EventDeleteList);
            ItemTapped = new Command(EventOpenList);
            MenuButtonTapped = new Command(EventMenuButtonTapped);

            JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "dashboard");
        }

        //methods
        public void UpdateList()
        {
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

        // command events
        private async void EventAddList()
        {
            Enable = false;
            await new NavigationService().PushModalPage(new AddListModal(), false);
            await Task.Delay(100);
            Enable = true;
        }

        private async void EventOpenList()
        {
            if (TappedItem != null && EnableTapped)
            {
                EnableTapped = false;
                var item = (PageList)TappedItem;
                TappedItem = null;
                await new NavigationService().PushPage(new AdvancedListPage(item.PageName));
                EnableTapped = true;
            }
        }

        private async void EventDeleteList()
        {
            Enable = false;
            await new NavigationService().PushModalPage(new DeleteListModal(), false);
            await Task.Delay(100);
            Enable = true;
        }

        private async void EventMenuButtonTapped()
        {
            Enable = false;
            await new NavigationService().PushModalPage(new MenuModal(), false);
            await Task.Delay(100);
            Enable = true;
        }
    }
}