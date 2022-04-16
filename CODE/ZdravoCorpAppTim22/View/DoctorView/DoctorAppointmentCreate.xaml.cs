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

namespace ZdravoCorpAppTim22.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentCreate.xaml
    /// </summary>
    public partial class DoctorAppointmentCreate : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string type;

        private int selectedDoctorID;

        public ObservableCollection<Room> RoomList { get; set; }
        public List<Room> rooms;


        public ObservableCollection<Patient> PatientList { get; set; }
        public List<Patient> patients;

        public DoctorAppointmentCreate(int selectedDoctorID)
        {
            InitializeComponent();
            this.DataContext = this;
            this.selectedDoctorID = selectedDoctorID;

            AppointmentTypeCBOX.ItemsSource = Enum.GetValues(typeof(AppointmentType));

            rooms = RoomController.Instance.GetAll();
            RoomList = new ObservableCollection<Room>(rooms);
            AppointmentType_Copy.ItemsSource = RoomList;


            patients = PatientController.Instance.GetAll();
            PatientList = new ObservableCollection<Patient>(patients);
            AppointmentType_Copy1.ItemsSource = PatientList;

        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
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

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(type == null)
            {
                return;
            }

            AppointmentType at = (AppointmentType)Enum.Parse(typeof(AppointmentType), type);
            Doctor doctor = DoctorController.Instance.GetByID(selectedDoctorID);
            Patient patient = AppointmentType_Copy1.SelectedItem as Patient;
            //DateTime? dt = datePicker.SelectedDate;
            Room room = AppointmentType_Copy.SelectedItem as Room;

            MedicalAppointment newMedicalAppointment = new MedicalAppointment(id,at,datePicker.SelectedDate.Value, datePicker.SelectedDate.Value, room, patient, doctor);
            MedicalAppointmentController.Instance.Create(newMedicalAppointment);

            //Doctor dr = DoctorController.Instance.GetByID(123);
            DoctorAppointments.CurDocAppointemntsObservable.Add(newMedicalAppointment);
            

            this.Close();

        }
    }
}
