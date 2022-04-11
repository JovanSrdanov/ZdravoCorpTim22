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
    /// Interaction logic for MakeAppointment.xaml
    /// </summary>
    public partial class MakeAppointment : Window
    {

        public ObservableCollection<Doctor> DoctorList { get; set; }
        public static DoctorController doctorController;
        public List<Doctor> doctors;
        public MakeAppointment()
        {
            InitializeComponent();
            ChooseAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
            doctorController = new DoctorController();
            doctors = doctorController.GetAll();
            DoctorList = new ObservableCollection<Doctor>(doctors);
            ChooseDoctor.ItemsSource = DoctorList;
        }

        private void NextAppontimentChoosing_Click(object sender, RoutedEventArgs e)
        {
            ChoosingAppointment choosingAppointment = new ChoosingAppointment();
            choosingAppointment.Show();
            this.Close();
        }
    }
}
