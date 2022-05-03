using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels;
using System.Windows;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;
using System;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomRelocationView : Page
    {
        readonly RoomDetailsView ParentPage;
        public RelocationViewModel RelocationViewModel;
        public Interval Interval;
        public Room SourceRoom;
        public Room DestinationRoom;
        public RoomRelocationView(Room sourceRoom, RoomDetailsView parent)
        {
            InitializeComponent();
            RelocationViewModel = new RelocationViewModel(parent.SelectedEquipment, sourceRoom);
            DataContext = RelocationViewModel;
            ParentPage = parent;
            SourceRoom = sourceRoom;
            StartTimeGroup.Visibility = Visibility.Hidden;
            EndTimeGroup.Visibility = Visibility.Hidden;
            ButtonPanel.Visibility = Visibility.Hidden;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {
            Interval.Start = ((CustomDatePicker)sender).SelectedDate;
            if (DateTime.Compare(Interval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = Interval.Start.ToString();
                EndTimeGroup.Visibility = Visibility.Visible;
                ButtonPanel.Visibility = Visibility.Hidden;
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
                ButtonPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonPanel.Visibility = Visibility.Hidden;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ParentPage);
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)DataGrid.SelectedItem;

            SelectEndTimeContent.Content = "";
            SelectStartTimeContent.Content = "";
            Interval.End = new DateTime();
            Interval.Start = new DateTime();
            StartTimeGroup.Visibility = Visibility.Hidden;
            EndTimeGroup.Visibility = Visibility.Hidden;
            ButtonPanel.Visibility = Visibility.Hidden;
            if (room != null)
            {
                DestinationRoom = room;
                StartTimeGroup.Visibility = Visibility.Visible;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationStartDateView = new SelectTimePage(this, SourceRoom, DestinationRoom);
            RelocationStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            NavigationService.Navigate(RelocationStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RelocationEndDateView = new SelectTimePage(this, SourceRoom, DestinationRoom, Interval.Start);
            RelocationEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            NavigationService.Navigate(RelocationEndDateView);
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            string message = RelocationViewModel.GetEquipmentErrors();
            if (message != null)
            {
                MessageBox.Show(message);
                return;
            }
            RelocationViewModel.RoomToRoom(SourceRoom, DestinationRoom, Interval);
            NavigationService.Navigate(new RoomDetailsView(SourceRoom));
        }
    }
}
