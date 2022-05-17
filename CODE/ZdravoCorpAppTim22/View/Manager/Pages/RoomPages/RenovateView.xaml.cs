using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RenovateView : Page
    {
        public RenovationViewModel ViewModel { get; set; }
        public RenovateView(Room room)
        {
            InitializeComponent();
            ViewModel = new RenovationViewModel(room);
            DataContext = ViewModel;
            
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(RoomType));
            ButtonSelectEndTime.IsEnabled = false;
            RoomEdit.IsEnabled = false;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {
            ViewModel.RenovationInterval = new Interval
            {
                Start = ((CustomDatePicker)sender).SelectedDate,
                End = ViewModel.RenovationInterval.End
            };

            if (DateTime.Compare(ViewModel.RenovationInterval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = ViewModel.RenovationInterval.Start.ToString();
                ButtonSelectEndTime.IsEnabled = true;
                RoomEdit.IsEnabled = false;
                ViewModel.RenovationInterval = new Interval
                {
                    Start = ViewModel.RenovationInterval.Start,
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
            ViewModel.RenovationInterval = new Interval
            {
                Start = ViewModel.RenovationInterval.Start,
                End = ((CustomDatePicker)sender).SelectedDate

            };
            if (DateTime.Compare(ViewModel.RenovationInterval.End, new DateTime()) > 0)
            {
                SelectEndTimeContent.Content = ViewModel.RenovationInterval.End.ToString();
                RoomEdit.IsEnabled = true;
            }
            else
            {
                RoomEdit.IsEnabled = false;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateStartDateView = new SelectTimePage(this, ViewModel.OldRoom);
            RenovateStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            this.NavigationService.Navigate(RenovateStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateEndDateView = new SelectTimePage(this, ViewModel.OldRoom, ViewModel.RenovationInterval.Start);
            RenovateEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            this.NavigationService.Navigate(RenovateEndDateView);
        }
    }
}
