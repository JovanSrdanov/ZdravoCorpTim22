using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentUpdate.xaml
    /// </summary>
    public partial class DoctorAppointmentUpdate : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int duration;
        private string type;

        public MedicalAppointment medAppointment;

        public ObservableCollection<Room> RoomList { get; set; }
        public List<Room> rooms;

        public ObservableCollection<Doctor> DoctorList { get; set; }
        public List<Doctor> doctors;

        public ObservableCollection<Patient> PatientList { get; set; }
        public List<Patient> patients;

        public DoctorAppointmentUpdate(MedicalAppointment medicalAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentTypeCBOX.ItemsSource = Enum.GetValues(typeof(AppointmentType));

            this.medAppointment = medicalAppointment;

            rooms = new List<Room>(RoomController.Instance.GetAll());


            RoomList = new ObservableCollection<Room>(rooms);
            Debug.WriteLine(rooms.Count + "");
            MessageBox.Show(rooms.Count + "");
            AppointmentType_Copy.ItemsSource = RoomList;

            doctors = new List<Doctor>(DoctorController.Instance.GetAll());
            DoctorList = new ObservableCollection<Doctor>(doctors);
            AppointmentType_Copy2.ItemsSource = DoctorList;

            patients = new List<Patient>(PatientController.Instance.GetAll());
            PatientList = new ObservableCollection<Patient>(patients);
            AppointmentType_Copy1.ItemsSource = PatientList;
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //AppointmentTypeCBOX.ItemSource = appEnum.GetValues(typeof AppointmentType);
        /* private void OnPropertyChanged(string propertyName = "")
         {
             PropertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }*/

        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public int Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (type == null)
            {
                return;
            }

            AppointmentType at = (AppointmentType)Enum.Parse(typeof(AppointmentType), type);
            Doctor doctor = AppointmentType_Copy2.SelectedItem as Doctor;
            Patient patient = AppointmentType_Copy1.SelectedItem as Patient;
            //DateTime? dt = datePicker.SelectedDate;
            ////////// JOVAN MENJAO ZA STEFANA, SSMISLI NESTO NOVO ZA DATETIMENOW
            Interval interval = new Interval();
            interval.Start = datePicker.SelectedDate.Value;
            interval.End = DateTime.Now;

            MedicalAppointment newMedicalAppointment = new MedicalAppointment(medAppointment.Id, at, interval, null, patient, doctor);
            this.Close();
        }
    }
}
