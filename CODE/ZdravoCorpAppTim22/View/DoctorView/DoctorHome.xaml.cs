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
    /// Interaction logic for DoctorHome.xaml
    /// </summary>
    public partial class DoctorHome : Window
    {
        public ObservableCollection<Doctor> DoctorListObservable { get; set; }
        public static DoctorController doctorController;
        public List<Doctor> DoctorList;
        public DoctorHome()
        {
            InitializeComponent();
            doctorController = new DoctorController();
            DoctorList = doctorController.GetAll();
            DoctorListObservable = new ObservableCollection<Doctor>(DoctorList);
            SelectDoctorCBOX.ItemsSource = DoctorListObservable;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointments doctorAppointments = new DoctorAppointments();
            doctorAppointments.Show();
            this.Close();
        }
    }
}
