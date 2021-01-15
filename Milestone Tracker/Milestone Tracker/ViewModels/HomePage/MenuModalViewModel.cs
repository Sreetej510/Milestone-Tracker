using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Login;
using Xamarin.Essentials;
using Xamarin.Forms;
using Milestone_Tracker.Models;
using System;
using System.Threading.Tasks;

namespace Milestone_Tracker.ViewModels.HomePage
{
    internal class MenuModalViewModel : BindableObject
    {
        public Grid ModalContainer { get; }
        public Grid ModalGrid { get; }
        public Command CloseMenuModal { get; }
        public Command LoginButtonTapped { get; }
        public Command RestoreList { get; }

        public string WebAPIkey = "AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI";
        public Command BackupList { get; set; }

        private bool closeEnabled;
        private ImageSource _loginStatuImage;

        public ImageSource LoginStatusImage
        {
            get { return _loginStatuImage; }
            set
            {
                _loginStatuImage = value;
                OnPropertyChanged();
            }
        }

        private string _loginText;

        public string LoginText
        {
            get { return _loginText; }
            set
            {
                _loginText = value;
                OnPropertyChanged();
            }
        }

        //Constructor
        public MenuModalViewModel(Grid modalGrid, Grid modalContainer)
        {
            ModalContainer = modalContainer;
            ModalGrid = modalGrid;
            CloseMenuModal = new Command(EventCloseThisModal);
            LoginButtonTapped = new Command(EventLoginButtonTapped);
            RestoreList = new Command(EventRestoreList);
            BackupList = new Command(EventBackupList);
            closeEnabled = true;
        }

        // methods
        public void LoginStatus()
        {
            if (Preferences.Get("LogStatus", "") != "loggedIn")
            {
                LoginText = "Login/SignUp";
                LoginStatusImage = "login_black.png";
            }
            else
            {
                LoginText = "LogOut";
                LoginStatusImage = "logout.png";
            }
        }

        // Events
        private async void EventLoginButtonTapped()
        {
            if (Preferences.Get("LogStatus", "") != "loggedIn")
            {
                Preferences.Set("LogStatus", "loggedIn");
                await new NavigationService().PushModalPage(new LoginPage());
            }
            else
            {
                Preferences.Set("LogStatus", "loggedOut");
                LoginText = "Login/SignUp";
                LoginStatusImage = "login_black.png";
            }
        }

        private async void EventRestoreList()
        {
            if (Preferences.Get("LogStatus", "") != "loggedIn")
            {
                await Application.Current.MainPage.DisplayAlert("Restore", "Login Needed !", "Ok");
            }
            else
            {
                try
                {
                    var upload = new UploadAndDownload();
                    await upload.RestoreFromStorage();
                    await Application.Current.MainPage.DisplayAlert("Restore", "Restore completed", "Ok");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Restore", "Restore Failed", "Ok");
                }
            }
        }

        private async void EventBackupList()
        {
            if (Preferences.Get("LogStatus", "") != "loggedIn")
            {
                await Application.Current.MainPage.DisplayAlert("Backup", "Login Needed !", "Ok");
            }
            else
            {
                try
                {
                    var upload = new UploadAndDownload();
                    await upload.UploadTOStorage();
                    await Application.Current.MainPage.DisplayAlert("Backup", "Backup completed", "Ok");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Backup", "Backup Failed", "Ok");
                }
            }
        }

        public async void EventCloseThisModal()
        {
            if (closeEnabled)
            {
                closeEnabled = false;
                await ModalContainer.TranslateTo(0, -200, 300);
                await ModalGrid.FadeTo(0, 50, Easing.CubicIn);
                new NavigationService().PopModalPage(false);
            }
        }
    }
}