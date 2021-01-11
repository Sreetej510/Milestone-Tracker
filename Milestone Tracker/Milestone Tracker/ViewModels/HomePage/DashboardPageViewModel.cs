﻿using Milestone_Tracker.Data;
using Milestone_Tracker.Models;
using Milestone_Tracker.Navigation;
using Milestone_Tracker.Views.Advanced_Lists;
using Milestone_Tracker.Views.HomePage;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Milestone_Tracker.ViewModels.HomePage
{
    internal class DashboardPageViewModel : BindableObject
    {
        public ObservableCollection<PageList> DashboardCollection { get; set; }
        public Command ItemTapped { get; set; }

        private PageList _tappedItem;

        public PageList TappedItem
        {
            get { return _tappedItem; }
            set
            {
                _tappedItem = value;
                OnPropertyChanged();
            }
        }

        private bool _enable;

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnPropertyChanged();
            }
        }

        public Command AddList { get; set; }
        public Command DeleteList { get; set; }
        public Command BackupList { get; set; }
        public ReadAndWriteJson JsonFIleActivities { get; }

        //Constructor
        public DashboardPageViewModel()
        {
            JsonFIleActivities = new ReadAndWriteJson("DashBoardList", "AppData", "dashboard");
            Task.Run(() =>
            {
                AddList = new Command(EventAddList);
                DeleteList = new Command(EventDeleteList);
                BackupList = new Command(EventBackupList);
                ItemTapped = new Command(EventOpenList);
            });

            Task.Run(() =>
            {
                DashboardCollection = new ObservableCollection<PageList>();
                UpdateList();
            });

            Enable = true;
        }

        private void EventBackupList(object obj)
        {
            var upload = new UploadAndDownload();
            Task.Run(() => upload.UploadTOStorage());
        }

        //methods
        public void UpdateList()
        {
            JObject jObject = JsonFIleActivities.ReadJson();

            var list = (JArray)jObject["list"];

            DashboardCollection.Clear();
            if (list != null)
            {
                foreach (var item in list)
                {
                    DashboardCollection.Add(new PageList(item["name"].ToString(), item["color"].ToString()));
                }
            }
        }

        // command events
        private async void EventAddList(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new AddListModal(), false);
        }

        private async void EventOpenList(object obj)
        {
            if (TappedItem != null)
            {
                Enable = false;
                var item = (PageList)TappedItem;
                TappedItem = null;
                await new NavigationService().PushPage(new AdvancedListPage(item.PageName));
            }
        }

        private async void EventDeleteList(object obj)
        {
            Enable = false;
            await new NavigationService().PushModalPage(new DeleteListModal(), false);
        }
    }
}