﻿using Milestone_Tracker.ViewModels.HomePage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milestone_Tracker.Views.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        private DashboardPageViewModel dashPageViewModal;

        public DashboardPage()
        {
            InitializeComponent();
            dashPageViewModal = new DashboardPageViewModel();
            BindingContext = dashPageViewModal;
        }

        protected override void OnAppearing()
        {
            dashPageViewModal.UpdateList();
            base.OnAppearing();
        }
    }
}