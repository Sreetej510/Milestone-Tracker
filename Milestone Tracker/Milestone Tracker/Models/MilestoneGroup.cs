﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Milestone_Tracker.Models
{
    class MilestoneGroup : List<Milestone>
    {
        public string Category { get; set; }
        public MilestoneGroup(string category)
        {
            Category = category;
        }
    }
}
