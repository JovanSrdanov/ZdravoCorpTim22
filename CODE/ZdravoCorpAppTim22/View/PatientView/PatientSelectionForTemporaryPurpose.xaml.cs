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

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientSelectionForTemporaryPurpose.xaml
    /// </summary>
    public partial class PatientSelectionForTemporaryPurpose : Window
    {
        public static Patient LoggedPatient { get; set; }
        public ObservableCollection<Patient> PatientList { get; set; }
        public static PatientController patientController;
        public List<Patient> patients;

        public PatientSelectionForTemporaryPurpose()
        {
            InitializeComponent();
            patientController = new PatientController();
            patients = patientController.GetAll();
            PatientList = new ObservableCollection<Patient>(patients);
            SelectPatientCB.ItemsSource = PatientList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoggedPatient = (Patient)SelectPatientCB.SelectedItem;
            PatientHome patientHome = new PatientHome();
            patientHome.Show();
            this.Close();
        }
    }
}
