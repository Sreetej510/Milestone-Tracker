using Xamarin.Forms;

namespace Milestone_Tracker.Navigation
{
    public interface INavigationService
    {
        void PushModalPage(Page page, bool animate);
        void PushPage(Page page, bool animate);
        void PopPage(bool animate);
        void PopModalPage(bool animate);

    }
}
