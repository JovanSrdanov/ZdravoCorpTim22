using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class CustomDatePicker : UserControl
    {
        public Room Room;
        public Appointment Appointment;
        public RenovationList RenView;
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

        public CustomDatePicker(Room room, DateTime startDate)
        {
            InitializeComponent();
            this.Room = room;
            DatePicker.DisplayDateStart = startDate;
            StartDate = startDate;
            DateTime EndDate = RenovationViewModel.GetLatestAvailableTime(room, startDate);
            if(DateTime.Compare(EndDate, StartDate) != 0)
            {
                DatePicker.DisplayDateEnd = EndDate;
            }
        }

        public DateTime GetDateTime()
        {
            DateTime dt = RenView != null ? RenView.SelectedAppointment.Interval.Start.Date : new DateTime();
            if(TimePicker != null)
            {
                dt += new TimeSpan(TimePicker.Hours, TimePicker.Minutes, 0);
            }
            return dt;
        }

        private void DateSelectionChanged(object sender, EventArgs e)
        {
            if (RenView.SelectedAppointment.Type == RoomAppointmentType.Free)
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
                if (DateTime.Compare(new DateTime(), StartDate) == 0)
                {
                    RenView = new RenovationList(Room, (DateTime)DatePicker.SelectedDate);
                }
                else
                {
                    RenView = new RenovationList(Room, (DateTime)DatePicker.SelectedDate, StartDate);
                }
                RenView.ListSelectionChanged += new EventHandler(DateSelectionChanged);
                AppointmentContent.Content = RenView;
                TimeContent.Content = null;
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = GetDateTime();
            Appointment app = RenView.SelectedAppointment;
            Interval interval = app.Interval;

            if (DateTime.Compare(time, interval.Start) >= 0 && DateTime.Compare(time, interval.End) < 0)
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
