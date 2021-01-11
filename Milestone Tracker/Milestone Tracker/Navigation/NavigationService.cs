using System.Linq;
using System.Threading.Tasks;
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

        public async Task PushModalPage(Page page, bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PushModalAsync(page, animate);
        }

        public async Task PushPage(Page page, bool animate = true)
        {
            var currentPage = GetCurrentPage();

            await currentPage.Navigation.PushAsync(page, animate);
        }

        public void PopToListPage(int count)
        {
            var currentPage = GetCurrentPage();
            for (int i = 0; i < count; i++)
            {
                currentPage.Navigation.PopModalAsync(false);
            }
        }
    }
}