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
    /// Interaction logic for ChoosingAppointment.xaml
    /// </summary>
    public partial class ChoosingAppointment : Window
    {
        public ChoosingAppointment()
        {
            InitializeComponent();
            SelectedDateLabel.Content = "Datum: " + MakeAppointment.selectedDateTime;
            SelectedDoctorLabel.Content = "Lekar: " + MakeAppointment.selectedDoctor.Name + " " + MakeAppointment.selectedDoctor.Surname;
            SelectedPriority.Content = "Prioritet: ";
            SelectedAppointmentType.Content = "Vrsta termina : " + MakeAppointment.selectedAppointmentType;
        }
    }
}
