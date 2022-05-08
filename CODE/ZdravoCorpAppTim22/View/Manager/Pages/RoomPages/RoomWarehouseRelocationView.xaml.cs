using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model.Utility;
using System.Windows;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;
using System;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;
using System.Collections.Generic;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomWarehouseRelocationView : Page
    {
        public RoomWarehouseRelocationViewModel ViewModel;
        
        public RoomWarehouseRelocationView(Room room, List<Equipment> selectedEquipment)
        {
            InitializeComponent();
            ViewModel = new RoomWarehouseRelocationViewModel(selectedEquipment, room);
            DataContext = ViewModel;
            EndTimeGroup.IsEnabled = false;
            ButtonPanel.IsEnabled = false;
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
                EndTimeGroup.IsEnabled = true;
                ButtonPanel.IsEnabled = false;
                ViewModel.Interval = new Interval
                {
                    Start = ViewModel.Interval.Start,
                    End = new DateTime()
                };
                SelectEndTimeContent.Content = "";
            }
            else
            {
                EndTimeGroup.IsEnabled = false;
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
                ButtonPanel.IsEnabled = true;
            }
            else
            {
                ButtonPanel.IsEnabled = false;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationStartDateView = new SelectTimePage(this, ViewModel.SourceRoom);
            RelocationStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            NavigationService.Navigate(RelocationStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationEndDateView = new SelectTimePage(this, ViewModel.SourceRoom, ViewModel.Interval.Start);
            RelocationEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            NavigationService.Navigate(RelocationEndDateView);
        }
    }
}
