using Firebase.Auth;
using Milestone_Tracker.Navigation;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.Login
{
    internal class LoginPageViewModel : BindableObject
    {
        public string WebAPIkey = "AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI";

        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
        public string SignupEmail { get; set; }
        public string SignupPassword { get; set; }
        public string SignupPasswordRetype { get; set; }

        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                OnPropertyChanged();
            }
        }

        public Command LoginClicked { get; }
        public Command ChangeToSignup { get; }
        public Command SignupClicked { get; }
        public Command ChangeToLogin { get; }
        public StackLayout LoginStack { get; }
        public StackLayout SignupStack { get; }

        public LoginPageViewModel(StackLayout loginStack, StackLayout signupStack)
        {
            LoginStack = loginStack;
            SignupStack = signupStack;
            LoginClicked = new Command(EventLogin);
            ChangeToSignup = new Command(EventChangeToSignup);
            SignupClicked = new Command(EventSignup);
            ChangeToLogin = new Command(EventChangeToLogin);
        }

        //animations
        private void EventChangeToSignup()
        {
            LoginStack.TranslateTo(0, -500, 300, Easing.Linear);
            ErrorMsg = null;
            SignupStack.TranslateTo(0, 0, 300, Easing.Linear);
        }

        private void EventChangeToLogin()
        {
            SignupStack.TranslateTo(0, 500, 300, Easing.Linear);
            ErrorMsg = null;
            LoginStack.TranslateTo(0, 0, 300, Easing.Linear);
        }

        //Login
        private async void EventLogin()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                if (LoginEmail != null && LoginPassword != null)
                {
                    try
                    {
                        var auth = await authProvider.SignInWithEmailAndPasswordAsync(LoginEmail, LoginPassword);
                        var content = await auth.GetFreshAuthAsync();
                        var serializedContent = JsonConvert.SerializeObject(content);
                        Preferences.Set("FirebaseRefreshToken", serializedContent);
                        Preferences.Set("LogStatus", "loggedIn");
                        await Application.Current.MainPage.DisplayAlert("", "Login Sucessful", "Ok");
                        new NavigationService().PopModalPage();
                    }
                    catch (Exception)
                    {
                        ErrorMsg = "Email or Password are not valid.";
                        Preferences.Set("LogStatus", "loggedOut");
                    }
                }
                else
                {
                    ErrorMsg = "Email and Password cannot be Empty";
                }
            }
            else
            {
                ErrorMsg = "No Internet Connection available";
            }
        }

        //Signup
        private async void EventSignup()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                if (SignupEmail != null && SignupPassword != null && SignupPassword == SignupPasswordRetype)
                {
                    try
                    {
                        var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(SignupEmail, SignupPassword);
                        await Application.Current.MainPage.DisplayAlert("", "SignUp Sucessful", "Ok");
                        EventChangeToLogin();
                    }
                    catch (Exception)
                    {
                        ErrorMsg = "Invalid Email or Account already Exists";
                    }
                }
                else
                {
                    if (SignupEmail == null || SignupPassword == null || SignupPasswordRetype == null)
                    {
                        ErrorMsg = "Email and Password fields cannot be empty";
                    }
                    else
                    {
                        ErrorMsg = "Passwords are not matching";
                    }
                }
            }
            else
            {
                ErrorMsg = "No Internet Connection available";
            }
        }
    }
}