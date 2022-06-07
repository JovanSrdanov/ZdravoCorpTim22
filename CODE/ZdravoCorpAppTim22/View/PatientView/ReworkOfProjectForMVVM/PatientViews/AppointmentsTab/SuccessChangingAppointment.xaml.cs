using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{

    public partial class SuccessChangingAppointment : Page
    {
        public SuccessChangingAppointment()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsPage appointmentsPage = new AppointmentsPage();
            this.NavigationService.Navigate(appointmentsPage);
        }
    }
}
