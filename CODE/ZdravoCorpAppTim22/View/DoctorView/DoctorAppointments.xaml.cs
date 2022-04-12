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
    /// Interaction logic for DoctorAppointments.xaml
    /// </summary>
    public partial class DoctorAppointments : Window
    {
        //public static DoctorController doctorController;
        // public static MedicalAppointmentController medicalAppointmentController;

        private List<MedicalAppointment> currentDoctorAppointments = new List<MedicalAppointment>();
        public static ObservableCollection<MedicalAppointment> CurDocAppointemntsObservable { get; set; }

        //allAppointments.add(MedicalAppointmentController.getById(123));


        //foreach (MedicalAppointment temp in medicalAppointmentController.getAll()) {
        //  }
        // private Doctor dr = doctorController.GetByID(123);
        //public List<MedicalAppointment> = dr.MedicalAppointment;

        public int selectedDoctorId;
        
        public DoctorAppointments(int id)
        {
            InitializeComponent();
            selectedDoctorId = id;
            List<Doctor> doctorList = DoctorController.Instance.GetAll();
            Doctor doctor = DoctorController.Instance.GetByID(id);
            List<MedicalAppointment> allMedicalAppointment = doctor.MedicalAppointment;
            currentDoctorAppointments = allMedicalAppointment;
            CurDocAppointemntsObservable = new ObservableCollection<MedicalAppointment>(currentDoctorAppointments);
            appointmentListGrid.ItemsSource = CurDocAppointemntsObservable;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentCreate appCreate = new DoctorAppointmentCreate(selectedDoctorId);
            appCreate.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointment medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                return;
            }
            MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
            CurDocAppointemntsObservable.Remove(medicalAppointment);
            MessageBox.Show("Apointment sa ID-em" + medicalAppointment.Id + "je obrisan");

        }
    }
}