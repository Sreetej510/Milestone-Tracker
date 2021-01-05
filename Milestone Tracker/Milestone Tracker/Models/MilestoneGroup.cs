using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Milestone_Tracker.Models
{
    public class MilestoneGroup : ObservableCollection<Milestone>
    {
        public string Category { get; set; }
        public MilestoneGroup(string category)
        {
            Category = category;
        }
    }
}
