using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientSelectionForTemporaryPurpose.xaml
    /// </summary>
    public partial class PatientSelectionForTemporaryPurpose : Window
    {
        public static Patient LoggedPatient { get; set; } = null;
        public ObservableCollection<Patient> PatientList { get; set; }
        public static PatientController patientController;
        public List<Patient> patients;

        public PatientSelectionForTemporaryPurpose()
        {
            InitializeComponent();
            patients = new List<Patient>(PatientController.Instance.GetAll());
            PatientList = new ObservableCollection<Patient>(patients);
            SelectPatientCB.ItemsSource = PatientList;         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoggedPatient = (Patient)SelectPatientCB.SelectedItem;
            ZdravoCorpTabs zdravoCorpTabs = new ZdravoCorpTabs();
            zdravoCorpTabs.Show();
            this.Close();
        }
    }
}
