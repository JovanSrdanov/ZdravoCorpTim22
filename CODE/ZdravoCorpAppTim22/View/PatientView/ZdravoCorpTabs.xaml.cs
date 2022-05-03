using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView
{
   
    public partial class ZdravoCorpTabs : Window
    {
        public static ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }
        public static MedicalAppointment MedicalAppointmentSelected { get;  set; }

        public List<MedicalAppointment> medicalAppointments;
        public ZdravoCorpTabs()
        {
            InitializeComponent();

            medicalAppointments = PatientController.Instance.GetByID(PatientSelectionForTemporaryPurpose.LoggedPatient.Id).MedicalAppointment;
            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(medicalAppointments);
            DataGrid.ItemsSource = MedicalAppointmentList;

        }

        private void AddAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment makeAppointment = new MakeAppointment();
            makeAppointment.ShowDialog();

        }

        private void RemoveAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentSelected = (MedicalAppointment)DataGrid.SelectedItem;
            if (MedicalAppointmentSelected == null)
            {
                return;
            }

            MedicalAppointmentController.Instance.DeleteByID(MedicalAppointmentSelected.Id);
            MedicalAppointmentList.Remove(MedicalAppointmentSelected);


        }

        private void ChangeAppointmentDateTime_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentSelected = (MedicalAppointment)DataGrid.SelectedItem;
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
            PatientSelectionForTemporaryPurpose.LoggedPatient = null;
        }

        private void RateZdravoCorp_Click(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
