using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView
{

    public partial class ZdravoCorpTabs : Window
    {


        public static ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }
        public static MedicalAppointment MedicalAppointmentSelected { get; set; }
        public static ObservableCollection<MedicalReceipt> MedicalReceiptsList { get; set; }

        public List<MedicalAppointment> medicalAppointments;

        public static Patient LoggedPatient { get; set; }

        public ZdravoCorpTabs(Patient patient)
        {
            InitializeComponent();
            LoggedPatient = patient;
            medicalAppointments = PatientController.Instance.GetByID(LoggedPatient.Id).MedicalAppointment;
            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(medicalAppointments);
            DataGridAppointment.ItemsSource = MedicalAppointmentList;


            MedicalRecord medRec = patient.medicalRecord;
            if (medRec == null)
            {
                MedicalReceiptsList = new ObservableCollection<MedicalReceipt>();
            }
            else
            {
                List<MedicalReceipt> MedicalReceipts = medRec.MedicalReceipt;
                MedicalReceiptsList = new ObservableCollection<MedicalReceipt>(MedicalReceipts);

            }

            DataGridReciepts.ItemsSource = MedicalReceiptsList;

            GeneralInfoForPatientMedicalRecord();

        }

        private void GeneralInfoForPatientMedicalRecord()
        {
            GridPatientName.Content = LoggedPatient.Name;
            GridPatientSurname.Content = LoggedPatient.Surname;
            GridPatientEmail.Content = LoggedPatient.Email;
            GridPatientJmbg.Content = LoggedPatient.Jmbg;
            GridPatientDateOfBirth.Content = LoggedPatient.Birthday.ToString("dd/MM/yyyy");
            GridPatientPhone.Content = LoggedPatient.Phone;
            GridPatientAdress.Content = LoggedPatient.Address.ToString();


            switch (LoggedPatient.Gender)
            {
                case Gender.male:
                    GridPatientGender.Content = "Muški";
                    break;
                case Gender.female:
                    GridPatientGender.Content = "Ženski";
                    break;
                case Gender.other:
                    GridPatientGender.Content = "Nepoznat";
                    break;
                default:
                    GridPatientGender.Content = "Nepoznat";
                    break;
            }


        }

        private void AddAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment makeAppointment = new MakeAppointment();
            makeAppointment.ShowDialog();

        }

        private void RemoveAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentSelected = (MedicalAppointment)DataGridAppointment.SelectedItem;
            if (MedicalAppointmentSelected == null)
            {
                return;
            }
            PatientController.Instance.AntiTroll(LoggedPatient);
            if (LoggedPatient == null)
            {

               

                Close();
                return;
            }

            MedicalAppointmentController.Instance.DeleteByID(MedicalAppointmentSelected.Id);
            MedicalAppointmentList.Remove(MedicalAppointmentSelected);

        }

        private void ChangeAppointmentDateTime_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentSelected = (MedicalAppointment)DataGridAppointment.SelectedItem;
            if (MedicalAppointmentSelected == null)
            {
                return;
            }

            if (MedicalAppointmentSelected.Interval.Start < DateTime.Now.AddDays(1))
            {
                MessageBox.Show("Ne moze da se pomeri termin 24h pre termina");
                return;
            }

            ChangeAppointment changeAppointment = new ChangeAppointment();
            changeAppointment.ShowDialog();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RateZdravoCorp_Click(object sender, RoutedEventArgs e)
        {

            ReviewTheHospital reviewTheHospital = new ReviewTheHospital();
            reviewTheHospital.ShowDialog();
        }


        private void ZdravoCorpTabs_OnClosing(object sender, CancelEventArgs e)
        {

            LoggedPatient = null;
            AuthenticationController.Instance.Logout();
            App.Current.MainWindow.Show();
        }
    }
}
