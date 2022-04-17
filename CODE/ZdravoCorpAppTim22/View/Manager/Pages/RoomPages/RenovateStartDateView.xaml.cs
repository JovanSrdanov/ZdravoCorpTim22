using Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels;
using ZdravoCorpAppTim22.View.Manager.Views;

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
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(RenovateView);
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = CustomDatePicker.GetDateTime();
            Appointment app = CustomDatePicker.RenView.SelectedAppointment;
            Interval interval = app.Interval;
            
            if(DateTime.Compare(time, interval.Start) >= 0 && DateTime.Compare(time, interval.End) < 0)
            {
                Debug.WriteLine(time);
                RenovateView.RenovationInterval = new Interval
                {
                    Start = time,
                    End = RenovateView.RenovationInterval.End
                };
                this.NavigationService.Navigate(RenovateView);
            }
            else
            {
                MessageBox.Show("Start time must be between: " + interval.Start.TimeOfDay + " and " + interval.End.TimeOfDay);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(RenovateView);
        }
    }
}
