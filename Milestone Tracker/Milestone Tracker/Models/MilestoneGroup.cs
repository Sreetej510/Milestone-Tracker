using System.Collections.ObjectModel;

namespace Milestone_Tracker.Models
{
    public class MilestoneGroup : ObservableCollection<Milestone>
    {
        public string Category { get; set; }
        public int GroupNumber { get; set; }
        public MilestoneGroup(string category, int groupNumber)
        {
            Category = category;
            GroupNumber = groupNumber;
        }
    }
}
