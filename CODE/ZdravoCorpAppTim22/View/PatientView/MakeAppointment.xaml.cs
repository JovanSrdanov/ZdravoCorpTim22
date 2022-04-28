using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for MakeAppointment.xaml
    /// </summary>
    public partial class MakeAppointment : Window
    {

        public ObservableCollection<Doctor> DoctorList { get; set; }
        public DoctorController doctorController;
        public List<Doctor> doctors;

        public static Doctor selectedDoctor;
        public static DateTime selectedDateTime;
        public static AppointmentType selectedAppointmentType;
        public static string selectedPriority;



        public MakeAppointment()
        {
            InitializeComponent();
            datePicker.DisplayDateStart = DateTime.Now.Date.AddDays(1);
            datePicker.SelectedDate = DateTime.Now.Date.AddDays(1);
            ChooseAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
            doctors = new List<Doctor>(DoctorController.Instance.GetAll());
            DoctorList = new ObservableCollection<Doctor>(doctors);
            ChooseDoctor.ItemsSource = DoctorList;

        }

        private void NextAppontimentChoosing_Click(object sender, RoutedEventArgs e)
        {
            selectedDoctor = (Doctor)ChooseDoctor.SelectedItem;
            selectedDateTime = (DateTime)datePicker.SelectedDate;
            selectedAppointmentType = (AppointmentType)ChooseAppointmentType.SelectedItem;
            selectedPriority = (bool)TimeRadioButton.IsChecked ? "Vreme" : "Lekar";

            ChoosingAppointment choosingAppointment = new ChoosingAppointment();
            choosingAppointment.Show();
            Close();
        }
    }
}
