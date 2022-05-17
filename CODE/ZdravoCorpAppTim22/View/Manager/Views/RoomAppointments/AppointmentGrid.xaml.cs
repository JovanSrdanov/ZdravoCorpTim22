using Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments
{
    public partial class AppointmentGrid : UserControl
    {
        public RoomAppointmentViewModel ViewModel;
        public Appointment SelectedAppointment;
        public event EventHandler ListSelectionChanged;

        public AppointmentGrid(Room room, DateTime date)
        {
            InitializeComponent();
            ViewModel = new RoomAppointmentViewModel(room, date);
            DataContext = ViewModel;
        }
        public AppointmentGrid(Room room1, Room room2, DateTime date)
        {
            InitializeComponent();
            ViewModel = new RoomAppointmentViewModel(room1, room2, date);
            DataContext = ViewModel;
        }

        public AppointmentGrid(Room room, DateTime date, DateTime startDate)
        {
            InitializeComponent();
            ViewModel = new RoomAppointmentViewModel(room, date, startDate);
            DataContext = ViewModel;
        }

        public AppointmentGrid(Room room1, Room room2, DateTime date, DateTime startDate)
        {
            InitializeComponent();
            ViewModel = new RoomAppointmentViewModel(room1, room2, date, startDate);
            DataContext = ViewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAppointment = (Appointment)DataGrid.SelectedItem;
            ListSelectionChanged(this, EventArgs.Empty);
        }
    }
}
