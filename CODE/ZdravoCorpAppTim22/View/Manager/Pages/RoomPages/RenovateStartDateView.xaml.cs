using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RenovateStartDateView : Page
    {
        private RenovateView RenovateView { get; set; }
        public CustomDatePicker CustomDatePicker { get; set; }
        
        public RenovateStartDateView(RenovateView parent, Room room)
        {
            InitializeComponent();
            RenovateView = parent;
            CustomDatePicker = new CustomDatePicker(room);
            SelectStartTimeContent.Content = CustomDatePicker;
            CustomDatePicker.DateSelectedEvent += StartDateSelected;
            CustomDatePicker.CancelEvent += DateSelectCancel;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {

            this.NavigationService.Navigate(RenovateView);
        }

        public void DateSelectCancel(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(RenovateView);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(RenovateView);
        }

    }
}
