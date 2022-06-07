using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{

    public partial class SuccessCreatingAppointment : Page
    {
        public SuccessCreatingAppointment()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AppointmentsPage appointmentsPage = new AppointmentsPage();
            this.NavigationService.Navigate(appointmentsPage);
        }
    }
}
