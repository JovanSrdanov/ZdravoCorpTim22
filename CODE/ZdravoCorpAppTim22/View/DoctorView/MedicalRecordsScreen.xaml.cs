using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MedicalRecordsScreen.xaml
    /// </summary>
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
