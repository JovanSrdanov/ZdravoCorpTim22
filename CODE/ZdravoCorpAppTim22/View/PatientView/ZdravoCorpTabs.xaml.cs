using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView
{
   
    public partial class ZdravoCorpTabs : Window
    {
        public static ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }
        public static MedicalAppointment MedicalAppointmentSelected { get;  set; }
        public static ObservableCollection<MedicalReceipt> MedicalReceiptsList { get;  set; }

        public List<MedicalAppointment> medicalAppointments;

        public static Patient LoggedPatient { get; set; }

        public ZdravoCorpTabs(Patient patient)
        {
            InitializeComponent();
            LoggedPatient = patient;
            medicalAppointments = PatientController.Instance.GetByID(patient.Id).MedicalAppointment;
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

            MedicalAppointmentController.Instance.DeleteByID(MedicalAppointmentSelected.Id);
            MedicalAppointmentList.Remove(MedicalAppointmentSelected);
            PatientController.Instance.AntiTroll(LoggedPatient, DateTime.Now);

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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
            AuthenticationController.Instance.Logout();
        }

        private void RateZdravoCorp_Click(object sender, RoutedEventArgs e)
        {
            ReviewTheHospital reviewTheHospital = new ReviewTheHospital();
            reviewTheHospital.ShowDialog();
        }
    }
}
