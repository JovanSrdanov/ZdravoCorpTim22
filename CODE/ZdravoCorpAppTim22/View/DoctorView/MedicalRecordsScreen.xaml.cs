using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class MedicalRecordsScreen : Window
    {
        private DoctorHomeScreen doctorHomeScreen;
        public MedicalRecordsScreen(DoctorHomeScreen doctorHomeScreen)
        {
            InitializeComponent();
            this.doctorHomeScreen = doctorHomeScreen;
            ObservableCollection<MedicalRecord> loggedInDoctorMedicalRecords = new ObservableCollection<MedicalRecord>
                (DoctorController.Instance.GetByID(DoctorHomeScreen.LoggedInDoctor.Id).MedicalRecord);
            setPatientInMedicalRecord(loggedInDoctorMedicalRecords);

            MedRecGrid.ItemsSource = loggedInDoctorMedicalRecords;
        }

        private void setPatientInMedicalRecord(ObservableCollection<MedicalRecord> loggedInDoctorMedicalRecords)        //mozda?
        {
            foreach (MedicalRecord medRec in loggedInDoctorMedicalRecords)
            {
                MedicalRecord medRecTemp = MedicalRecordController.Instance.GetByID(medRec.Id);
                medRec.Patient = medRecTemp.Patient;
            }
        }

        private bool isMedicalRecordSelected(MedicalRecord medicalRecord)       //ne pomeraj
        {
            bool returnValue = true;
            if (medicalRecord == null)
            {
                MessageBox.Show("Please select a medical record", "Medical record list", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void OpenBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            MedicalRecord medicalRecord = MedRecGrid.SelectedItem as MedicalRecord;
            if(!(isMedicalRecordSelected(medicalRecord))) return;

            MedicalRecord medRecTemp = MedicalRecordController.Instance.GetByID(medicalRecord.Id);
            medicalRecord.Patient = medRecTemp.Patient;

            //-1 ako doktor ne moze da pravi izvestaj, u suprotnom se konstruktoru prosledjuje 1
            MedicalRecordView medicalRecordView = new MedicalRecordView(-1, medicalRecord.Patient.Id, null, this);
            medicalRecordView.Owner = this;                                                                 
            medicalRecordView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordView.Show();
            this.Hide();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            doctorHomeScreen.Show();
            this.Close();
        }
 
        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            //DoctorHome.doctorHome.Show();
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
