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
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class CustomDatePicker : UserControl
    {
        public Room Room;
        public Appointment Appointment;
        public RenovationList RenView;
        public CustomTimePicker TimePicker;

        public CustomDatePicker(Room room)
        {
            InitializeComponent();
            this.Room = room;

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

        private void StartDateSelectionChanged(object sender, EventArgs e)
        {
            if (RenView.SelectedAppointment.Type == RoomAppointmentType.Free)
            {
                TimePicker = new CustomTimePicker();
                TimeStartContent.Content = TimePicker;
            }
            else
            {
                TimePicker = null;
                TimeStartContent.Content = null;
            }
        }

        private void RenovationStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RenovationStart.SelectedDate != null)
            {
                RenView = new RenovationList(Room, (DateTime)RenovationStart.SelectedDate);
                RenView.ListSelectionChanged += new EventHandler(StartDateSelectionChanged);
                AppointmentContent.Content = RenView;
                TimeStartContent.Content = null;
            }
        }
    }
}
