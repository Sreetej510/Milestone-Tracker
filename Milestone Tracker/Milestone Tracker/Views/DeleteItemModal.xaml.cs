using Milestone_Tracker.Models;
using Milestone_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views
{
    public partial class DeleteItemModal : ContentPage
    {
        public DeleteItemModal(Milestone item, PopulateList populateList)
        {
            BindingContext = new DeleteItemModalViewModel(item,populateList);
            InitializeComponent();
        }

    }
}