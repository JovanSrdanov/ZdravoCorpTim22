using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class MedicalRecordsScreen : Window
    {
        public ObservableCollection<MedicalRecord> MedRecList { get; set; }
        private DoctorHomeScreen doctorHomeScreen;
        public MedicalRecordsScreen(DoctorHomeScreen doctorHomeScreen)
        {
            InitializeComponent();
            this.doctorHomeScreen = doctorHomeScreen;
            MedRecList = new ObservableCollection<MedicalRecord>(DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId).MedicalRecord);

            foreach (MedicalRecord medRec in MedRecList)
            {
                MedicalRecord medRecTemp = MedicalRecordController.Instance.GetByID(medRec.Id);
                medRec.Patient = medRecTemp.Patient;
            }
            MedRecGrid.ItemsSource = MedRecList;
        }

        private void OpenBtnClick(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = MedRecGrid.SelectedItem as MedicalRecord;
            if (medicalRecord == null)
            {
                MessageBox.Show("Please select a medical record", "Medical record list", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicalRecord medRecTemp = MedicalRecordController.Instance.GetByID(medicalRecord.Id);
            medicalRecord.Patient = medRecTemp.Patient;

            MedicalRecordView medicalRecordView = new MedicalRecordView(-1, medicalRecord.Patient.Id, null, this);
            medicalRecordView.Owner = this;
            medicalRecordView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordView.Show();
            this.Hide();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            doctorHomeScreen.Show();
            this.Close();
        }
 
        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
