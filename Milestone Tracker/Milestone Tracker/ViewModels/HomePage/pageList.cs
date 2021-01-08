namespace Milestone_Tracker.ViewModels.HomePage
{
    internal class pageList
    {
        public string PageName { get; set; }
        public char FirstLetter { get; set; }
        public pageList(string pageName)
        {
            PageName = pageName;
            FirstLetter = char.ToUpper(PageName[0]);
        }
    }
}