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

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {
        public PatientHome()
        {
            InitializeComponent();
            PatientController patientController = new PatientController();
            List<Patient> patients = patientController.GetAll();
            lvDataBinding.ItemsSource = patients;
            
        }

    }
}
