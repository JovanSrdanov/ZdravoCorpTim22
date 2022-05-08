using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class WarehouseRelocationView : Page
    {
        public WarehouseRelocationViewModel ViewModel;
        public WarehouseRelocationView(List<Equipment> selectedEquipment)
        {
            InitializeComponent();
            ViewModel = new WarehouseRelocationViewModel(selectedEquipment);
            DataContext = ViewModel;
            StartTimeGroup.IsEnabled = false;
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)DataGrid.SelectedItem;

            SelectEndTimeContent.Content = "";
            SelectStartTimeContent.Content = "";
            ViewModel.Interval = new Interval()
            {
                End = new DateTime(),
                Start = new DateTime()
            };
            StartTimeGroup.IsEnabled = false;
            EndTimeGroup.IsEnabled = false;
            ButtonPanel.IsEnabled = false;
            if (room != null)
            {
                ViewModel.DestinationRoom = room;
                StartTimeGroup.IsEnabled = true;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationStartDateView = new SelectTimePage(this, ViewModel.DestinationRoom);
            RelocationStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            NavigationService.Navigate(RelocationStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationEndDateView = new SelectTimePage(this, ViewModel.DestinationRoom, ViewModel.Interval.Start);
            RelocationEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            NavigationService.Navigate(RelocationEndDateView);
        }
    }
}
