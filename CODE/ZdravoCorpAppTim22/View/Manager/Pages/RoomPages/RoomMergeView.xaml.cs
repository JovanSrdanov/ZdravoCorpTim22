using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomMergeView : Page
    {
        public RoomMergeViewModel ViewModel;
        public RoomMergeView(Room room_1, Room room_2)
        {
            InitializeComponent();
            ViewModel = new RoomMergeViewModel(room_1, room_2);
            DataContext = ViewModel;
            
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(RoomType));
            ButtonSelectEndTime.IsEnabled = false;
            RoomEdit.IsEnabled = false;

            SurfaceInput.IsEnabled = false;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {
            ViewModel.Interval = new Interval
            {
                Start = ((CustomDatePicker)sender).SelectedDate,
                End = ViewModel.Interval.End
            };

            if (DateTime.Compare(ViewModel.Interval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = ViewModel.Interval.Start.ToString();
                ButtonSelectEndTime.IsEnabled = true;
                RoomEdit.IsEnabled = false;
                ViewModel.Interval = new Interval
                {
                    Start = ViewModel.Interval.Start,
                    End = new DateTime()
                };
                SelectEndTimeContent.Content = "";
            }
            else
            {
                ButtonSelectEndTime.IsEnabled = false;
            }
        }
        public void EndDateSelected(object sender, EventArgs e)
        {
            ViewModel.Interval = new Interval
            {
                Start = ViewModel.Interval.Start,
                End = ((CustomDatePicker)sender).SelectedDate

            };
            if (DateTime.Compare(ViewModel.Interval.End, new DateTime()) > 0)
            {
                SelectEndTimeContent.Content = ViewModel.Interval.End.ToString();
                RoomEdit.IsEnabled = true;
            }
            else
            {
                RoomEdit.IsEnabled = false;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateStartDateView = new SelectTimePage(this, ViewModel.Room_1, ViewModel.Room_2);
            RenovateStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            this.NavigationService.Navigate(RenovateStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateEndDateView = new SelectTimePage(this, ViewModel.Room_1, ViewModel.Room_2, ViewModel.Interval.Start);
            RenovateEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            this.NavigationService.Navigate(RenovateEndDateView);
        }
    }
}
