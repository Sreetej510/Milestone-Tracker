using Milestone_Tracker.ViewModels.Login;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }
    }
}