using Controller;
using Model;
using System;
using System.Collections.Generic;
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
        public static DoctorController doctorController;
        public static MedicalAppointmentController medicalAppointmentController;

        /*public List<MedicalAppointment> currentDoctorAppointments = new List<MedicalAppointment>();
        public List<MedicalAppointment> allAppointments = medicalAppointmentController.get

        foreach (MedicalAppointment temp in medicalAppointmentController.getAll()) {
            }*/
        private Doctor dr = doctorController.GetByID(123);
        //public List<MedicalAppointment> = dr.MedicalAppointment;
        

        public DoctorAppointments()
        {
            InitializeComponent();
            List<Doctor> doctorList = doctorController.GetAll();
            appointmentListGrid.ItemsSource = doctorList;
        }
    }
}