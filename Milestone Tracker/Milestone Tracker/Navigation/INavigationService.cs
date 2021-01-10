using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.Navigation
{
    public interface INavigationService
    {
        Task PushModalPage(Page page, bool animate);
        Task PushPage(Page page, bool animate);
        void PopPage(bool animate);
        void PopModalPage(bool animate);
        void PopToListPage(int count);

    }
}
