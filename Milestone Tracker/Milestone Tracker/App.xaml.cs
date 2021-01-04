using Autofac;
using FreshMvvm;
using Milestone_Tracker.PageModels;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "SwipeView_Experimental" });

            var mainPage = FreshPageModelResolver.ResolvePageModel<MainPageModel>();

            var navigationContainer = new FreshNavigationContainer(mainPage);

            MainPage = navigationContainer;

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
