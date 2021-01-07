using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Milestone_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "SwipeView_Experimental", "Shapes_Experimental" });

            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            
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
