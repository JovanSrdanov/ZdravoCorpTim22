using Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class RelocationView : Page
    {
        readonly WarehouseView ParentPage;
        public RelocationViewModel RelocationViewModel;
        public Interval Interval;
        public Room DestinationRoom;
        public RelocationView(WarehouseView parent)
        {
            InitializeComponent();
            RelocationViewModel = new RelocationViewModel(parent.SelectedEquipment);
            DataContext = RelocationViewModel;
            ParentPage = parent;
            StartTimeGroup.Visibility = Visibility.Hidden;
            EndTimeGroup.Visibility = Visibility.Hidden;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {
            Interval.Start = ((CustomDatePicker)sender).SelectedDate;
            if (DateTime.Compare(Interval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = Interval.Start.ToString();
                EndTimeGroup.Visibility = Visibility.Visible;
                //RoomEdit.Visibility = Visibility.Hidden;
                Interval.End = new DateTime();
                SelectEndTimeContent.Content = "";
            }
            else
            {
                EndTimeGroup.Visibility = Visibility.Hidden;
            }
        }

        public void EndDateSelected(object sender, EventArgs e)
        {
            Interval.End = ((CustomDatePicker)sender).SelectedDate;
            if (DateTime.Compare(Interval.End, new DateTime()) > 0)
            {
                SelectEndTimeContent.Content = Interval.End.ToString();
                //RoomEdit.Visibility = Visibility.Visible;
            }
            else
            {
                //RoomEdit.Visibility = Visibility.Hidden;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ParentPage);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)DataGrid.SelectedItem;
            if(room != null)
            {
                DestinationRoom = room;
                StartTimeGroup.Visibility = Visibility.Visible;
            }
            else
            {
                StartTimeGroup.Visibility = Visibility.Hidden;
                EndTimeGroup.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationStartDateView = new SelectTimePage(this, DestinationRoom);
            RelocationStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            this.NavigationService.Navigate(RelocationStartDateView);
        }

        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationEndDateView = new SelectTimePage(this, DestinationRoom, Interval.Start);
            RelocationEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            this.NavigationService.Navigate(RelocationEndDateView);
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
