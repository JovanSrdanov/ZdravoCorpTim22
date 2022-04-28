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
    /// Interaction logic for ZdravoCorpTabs.xaml
    /// </summary>
    public partial class ZdravoCorpTabs : Window
    {
        public static ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }
        public List<MedicalAppointment> medicalAppointments;
        public ZdravoCorpTabs()
        {
            InitializeComponent();
          

 

            medicalAppointments = PatientController.Instance.GetByID(PatientSelectionForTemporaryPurpose.LoggedPatient.Id).MedicalAppointment;

            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(medicalAppointments);
            dataGrid.ItemsSource = MedicalAppointmentList;
        }

        private void AddAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment makeAppointment = new MakeAppointment();
            makeAppointment.Show();

        }

        private void RemoveAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointment medicalAppointmentTemp = (MedicalAppointment)dataGrid.SelectedItem;
            if (medicalAppointmentTemp == null)
            {
                return;
            }
            MedicalAppointmentController.Instance.DeleteByID(medicalAppointmentTemp.Id);
            MedicalAppointmentList.Remove(medicalAppointmentTemp);


        }
    }
}
