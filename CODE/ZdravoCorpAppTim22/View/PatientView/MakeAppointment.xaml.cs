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
            TabDoctor.IsEnabled = false;
            TabDate.IsEnabled = false;
            TabPriority.IsEnabled = false;


            ChooseAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType));

            

            datePicker.DisplayDateStart = DateTime.Now.Date.AddDays(1);
            datePicker.SelectedDate = DateTime.Now.Date.AddDays(1);

        }


        private void AppointmentNext_Click(object sender, RoutedEventArgs e)
        {
           
            selectedAppointmentType = (AppointmentType)ChooseAppointmentType.SelectedItem;        
            DoctorList = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            if (selectedAppointmentType == AppointmentType.operation)
            {
                ObservableCollection<Doctor> temporaryDoctors = new ObservableCollection<Doctor>();

                foreach (Doctor doctor in DoctorList)
                {
                    if (doctor.DoctorType == DoctorSpecialisationType.specialist)
                    {
                        temporaryDoctors.Add(doctor);
                    }
                }
                DoctorList = temporaryDoctors;
            }
           
            ChooseDoctor.ItemsSource = DoctorList;
            ChooseDoctor.SelectedIndex = 0;
            TabDoctor.IsEnabled = true;
            TabDoctor.IsSelected = true;
            TabType.IsEnabled = false;
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DoctorBack_Click(object sender, RoutedEventArgs e)
        {
            TabType.IsEnabled = true;
            TabType.IsSelected = true;
            TabDoctor.IsEnabled = false;
        }

        private void DoctorNext_Click(object sender, RoutedEventArgs e)
        {
            TabDate.IsEnabled = true;
            TabDate.IsSelected = true;
            TabDoctor.IsEnabled = false;
        }

        private void DateBack_Click(object sender, RoutedEventArgs e)
        {
            TabDoctor.IsEnabled = true;
            TabDoctor.IsSelected = true;
            TabDate.IsEnabled = false;
        }

        private void DateNext_Click(object sender, RoutedEventArgs e)
        {
            TabPriority.IsEnabled = true;
            TabPriority.IsSelected = true;
            TabDate.IsEnabled = false;
        }

        private void PriorityBack_Click(object sender, RoutedEventArgs e)
        {
            TabDate.IsEnabled = true;
            TabDate.IsSelected = true;
            TabPriority.IsEnabled = false;

        }

        private void PriorityFinish_Click(object sender, RoutedEventArgs e)
        {

            selectedDoctor = (Doctor)ChooseDoctor.SelectedItem;
            selectedDateTime = (DateTime)datePicker.SelectedDate;
            
            selectedPriority = (bool)TimeRadioButton.IsChecked ? "Vreme" : "Lekar";
            ChoosingAppointment choosingAppointment = new ChoosingAppointment();
            choosingAppointment.Show();
            Close();

        }
    }
}
