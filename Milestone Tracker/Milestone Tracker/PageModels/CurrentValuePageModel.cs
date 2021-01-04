using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Milestone_Tracker.PageModels
{
    class CurrentValuePageModel : FreshBasePageModel
    {
        public Models.Milestone Item;
        public override void Init(object initData)
        {
            base.Init(initData);
            Item = (Models.Milestone)initData;
        }
    }
}
