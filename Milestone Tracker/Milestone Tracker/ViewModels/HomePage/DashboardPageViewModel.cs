using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Milestone_Tracker.ViewModels.HomePage
{
    class DashboardPageViewModel
    {
        public ObservableCollection<pageList> DashboardCollection { get; set; }
        public DashboardPageViewModel()
        {
            DashboardCollection = new ObservableCollection<pageList>();
            DashboardCollection.Add(new pageList("Hello"));
            DashboardCollection.Add(new pageList("This"));
            DashboardCollection.Add(new pageList("is a"));
            DashboardCollection.Add(new pageList("Test"));
            DashboardCollection.Add(new pageList("Test"));
        }
    }
}
