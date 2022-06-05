using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
   
    public partial class AppointmentsPage : Page
    {
        private AppointmentsPageViewModel appointmentsPageViewModel;
        public AppointmentsPage()
        {
            InitializeComponent();
            appointmentsPageViewModel = new AppointmentsPageViewModel();
            this.DataContext = appointmentsPageViewModel;
        }


        private void ChangeAppointment_OnClick(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentsViewModel medicalAppointmentsViewModel =
                (MedicalAppointmentsViewModel)this.DataGridAppointment.SelectedItem;
          
            ChoosingDateView choosingDateView = new ChoosingDateView(medicalAppointmentsViewModel);
            this.NavigationService.Navigate(choosingDateView);
        }
    }
}
