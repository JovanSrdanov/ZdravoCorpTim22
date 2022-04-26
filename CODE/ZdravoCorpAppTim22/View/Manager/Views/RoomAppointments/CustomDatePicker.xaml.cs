using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments
{
    public partial class CustomDatePicker : UserControl
    {
        public Room Room;
        public Room SecondRoom;
        public Appointment Appointment;
        public AppointmentGrid AppointmentGrid;
        public CustomTimePicker TimePicker;

        public DateTime StartDate;
        public DateTime SelectedDate;
        
        public event EventHandler DateSelectedEvent;
        public event EventHandler CancelEvent;

        public CustomDatePicker(Room room)
        {
            InitializeComponent();
            this.Room = room;
        }

        public CustomDatePicker(Room room, Room secondRoom)
        {
            InitializeComponent();
            this.Room = room;
            this.SecondRoom = secondRoom;
        }

        public CustomDatePicker(Room room, DateTime startDate)
        {
            InitializeComponent();
            this.Room = room;
            DatePicker.DisplayDateStart = startDate;
            StartDate = startDate;
            DateTime EndDate = RoomAppointmentViewModel.GetLatestAvailableTime(room, startDate);
            if(DateTime.Compare(EndDate, StartDate) != 0)
            {
                DatePicker.DisplayDateEnd = EndDate;
            }
        }

        public CustomDatePicker(Room room, Room secondRoom, DateTime startDate)
        {
            InitializeComponent();
            this.Room = room;
            this.SecondRoom = secondRoom;
            DatePicker.DisplayDateStart = startDate;
            StartDate = startDate;
            DateTime EndDate = RoomAppointmentViewModel.GetLatestAvailableTime(room, secondRoom, startDate);
            if (DateTime.Compare(EndDate, StartDate) != 0)
            {
                DatePicker.DisplayDateEnd = EndDate;
            }
        }

        public DateTime GetDateTime()
        {
            DateTime dt = AppointmentGrid != null ? AppointmentGrid.SelectedAppointment.Interval.Start.Date : new DateTime();
            if(TimePicker != null)
            {
                dt += new TimeSpan(TimePicker.Hours, TimePicker.Minutes, 0);
            }
            return dt;
        }

        private void DateSelectionChanged(object sender, EventArgs e)
        {
            if (AppointmentGrid.SelectedAppointment.Type == RoomAppointmentType.Free)
            {
                TimePicker = new CustomTimePicker();
                TimeContent.Content = TimePicker;
            }
            else
            {
                TimePicker = null;
                TimeContent.Content = null;
            }
        }

        private void Start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate != null)
            {
                if (new DateTime() == StartDate)
                {
                    if(SecondRoom == null)
                    {
                        AppointmentGrid = new AppointmentGrid(Room, (DateTime)DatePicker.SelectedDate);
                    }else
                    {
                        AppointmentGrid = new AppointmentGrid(Room, SecondRoom, (DateTime)DatePicker.SelectedDate);
                    }
                }
                else
                {
                    if(SecondRoom == null)
                    {
                        AppointmentGrid = new AppointmentGrid(Room, (DateTime)DatePicker.SelectedDate, StartDate);
                    }
                    else
                    {
                        AppointmentGrid = new AppointmentGrid(Room, SecondRoom, (DateTime)DatePicker.SelectedDate, StartDate);
                    }
                }
                AppointmentGrid.ListSelectionChanged += new EventHandler(DateSelectionChanged);
                AppointmentContent.Content = AppointmentGrid;
                TimeContent.Content = null;
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = GetDateTime();
            if(AppointmentGrid == null)
            {
                return;
            } 
            Appointment app = AppointmentGrid.SelectedAppointment;
            Interval interval = app.Interval;

            if (time >= interval.Start && time <= interval.End)
            {
                this.SelectedDate = time;
                this.DateSelectedEvent?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Start time must be between: " + interval.Start.TimeOfDay + " and " + interval.End.TimeOfDay);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
