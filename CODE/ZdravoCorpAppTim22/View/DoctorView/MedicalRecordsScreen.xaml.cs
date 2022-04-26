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

            MedicalRecordView medicalRecordView = new MedicalRecordView(-1, medicalRecord.Patient.Id, null, this);
            medicalRecordView.Owner = this;
            medicalRecordView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordView.Show();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            doctorHomeScreen.Show();
            this.Close();
        }
        
        private void MedRecListClose(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}
