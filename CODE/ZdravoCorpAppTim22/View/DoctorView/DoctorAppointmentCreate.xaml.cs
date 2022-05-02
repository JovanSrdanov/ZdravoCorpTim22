using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorAppointmentCreate : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private int id;
        private string type;
        //private int selectedDoctorID;
        private DoctorAppointments doctorAppointments;
        private Doctor doctor;

        //date time 
        private string date;
        private string time;
        private DateTime dateTime;
        //date time 

        public ObservableCollection<Room> RoomList { get; set; }
        public List<Room> rooms;
        public ObservableCollection<Patient> PatientList { get; set; }
        public List<Patient> patients;

        public DoctorAppointmentCreate(DoctorAppointments doctorAppointments)
        {
            InitializeComponent();
            this.DataContext = this;
            //this.selectedDoctorID = selectedDoctorID;
            this.doctorAppointments = doctorAppointments;

            List<AppointmentType> appointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            ObservableCollection<AppointmentType> newAppointmentTypes = new ObservableCollection<AppointmentType>(appointmentTypes);
            newAppointmentTypes.Remove(AppointmentType.Operation);
            doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);

            if (doctor.DoctorType == DoctorSpecialisationType.specialist)
            {
                AppointmentTypeCBOX.ItemsSource = Enum.GetValues(typeof(AppointmentType));
            }
            else
            {
                AppointmentTypeCBOX.ItemsSource = newAppointmentTypes;
            }

            rooms = new List<Room>(RoomController.Instance.GetAll());
            RoomList = new ObservableCollection<Room>(rooms);
            AppointmentType_Copy.ItemsSource = RoomList;

            patients = new List<Patient>(PatientController.Instance.GetAll());
            PatientList = new ObservableCollection<Patient>(patients);
            AppointmentType_Copy1.ItemsSource = PatientList;
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = AppointmentType_Copy1.SelectedItem as Patient;
            Room room = AppointmentType_Copy.SelectedItem as Room;
            date = datePicker.Text;
            time = TimeComboBox.Text;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                dateTime = DateTime.Parse(date + " " + time);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the valid time", "Create appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
#pragma warning restore CS0168 // Variable is declared but never used

            if (type == null || doctor == null || patient == null || room == null || datePicker.SelectedDate == null || time == "")
            {
                MessageBox.Show("Please fill out all fields", "Create appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AppointmentType at = (AppointmentType)Enum.Parse(typeof(AppointmentType), type);

            //datePicker.SelectedDate.Value
            //promeniti konstruktor za medical appointment, ne treba da ide Id vise, automatski se inkrementira za svaki create room, promeniti i kod Jovana

            Interval interval = new Interval();
            interval.Start = dateTime;
            interval.End = dateTime;

            MedicalAppointment newMedicalAppointment = new MedicalAppointment(-1,at, interval, room, patient, doctor);
            MedicalAppointmentController.Instance.Create(newMedicalAppointment);
            DoctorAppointments.CurDocAppointemntsObservable.Add(newMedicalAppointment);

            doctorAppointments.Show();
            this.Close();

        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            CancelDialogBox();
        }

        private void CancelDialogBox()
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Create appointment", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    doctorAppointments.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        private void DoctorAppointmentCreateClose(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}
