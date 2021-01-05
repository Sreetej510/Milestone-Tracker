using System.Linq;
using Xamarin.Forms;

namespace Milestone_Tracker.Navigation
{
    public class NavigationService : INavigationService
    {
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }

        public async void PopModalPage(bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PopModalAsync(animate);
        }

        public async void PopPage(bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PopAsync(animate);
        }

        public async void PushModalPage(Page page, bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PushModalAsync(page, animate);
        }

        public async void PushPage(Page page, bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PushAsync(page, animate);
        }

    }
}
