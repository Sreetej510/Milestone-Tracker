using Firebase.Auth;
using Milestone_Tracker.Navigation;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Login
{
    internal class LoginPageViewModel : BindableObject
    {
        public Command Login { get; set; }

        public string WebAPIkey = "AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI";

        private string username;
        private string password;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public LoginPageViewModel()
        {
            Login = new Command(EventLogin);
        }

        private async void EventLogin(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Username, Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                new NavigationService().PopPage();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
    }
}