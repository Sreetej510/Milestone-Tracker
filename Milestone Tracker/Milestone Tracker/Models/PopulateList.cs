﻿using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Milestone_Tracker.Models
{
    public class PopulateList : BindableObject
    {
        private string ListName;
        private ObservableCollection<MilestoneGroup> _milestonesList;
        private int GroupIndex;
        
        public ObservableCollection<MilestoneGroup> MilestonesList
        {
            get
            {
                return _milestonesList;
            }
            set
            {
                _milestonesList = value;
                OnPropertyChanged();
            }
        }
         
        public PopulateList(string listname)
        {
            this.ListName = listname;
            MilestonesList = new ObservableCollection<MilestoneGroup>();
            MilestonesList.Add(new MilestoneGroup("Eliminations", MilestonesList.Count)
            {
                new Milestone(MilestonesList.Count,"Kills with smg", new int[] {10, 20, 30, 40, 50}, 22),
                new Milestone(MilestonesList.Count,"Kills with pickaxe", new int[] {5, 10, 25, 45, 90}, 52),
                new Milestone(MilestonesList.Count,"Eliminations With Common or Uncommon Weapons", new int[] {10, 25, 50, 100,300}, 60)
            });

            MilestonesList.Add(new MilestoneGroup("Damage", MilestonesList.Count)
            {
                new Milestone(MilestonesList.Count,"Damage with smg", new int[] {10, 20, 30, 40, 50}, 32),
                new Milestone(MilestonesList.Count,"Damage with pickaxe", new int[] {5, 10, 25, 45, 90}, 2),
                new Milestone(MilestonesList.Count,"Damage with common and uncommon weapons and would be great if it is with falldamage", new int[] {10, 25, 50, 100,300}, 20)
            });

            MilestonesList.Add(new MilestoneGroup("Burn", MilestonesList.Count)
            {
                new Milestone(MilestonesList.Count,"Burn Enemies", new int[] {10, 50, 150, 300, 500}, 225),
                new Milestone(MilestonesList.Count,"Burn Enemy stucture", new int[] {50, 200, 500, 1000, 2500}, 380)
            });

        }
    }
}