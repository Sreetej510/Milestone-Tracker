using Milestone_Tracker.ViewModels.HomePage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddListModal : ContentPage
    {
        public AddListModal()
        {
            InitializeComponent();
            TransitionModalIn();
            BindingContext = new AddListModalViewModel(modalGrid);            
        }


        private async void TransitionModalIn()
        {
            await modalContainer.TranslateTo(0, 0, 300);
        }

    }
}