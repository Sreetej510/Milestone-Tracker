using Milestone_Tracker.Views.HomePage;
using Xamarin.Forms;

namespace Milestone_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "SwipeView_Experimental", "Brush_Experimental" });

            MainPage = new NavigationPage(new DashboardPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}