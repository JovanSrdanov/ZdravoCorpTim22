using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

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

        private void RemoveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (PatientController.Instance.AntiTroll((Patient)AuthenticationController.Instance.GetLoggedUser()))
            {
                MessageBox.Show("Pacijent je blokiran!");
                List<Window> windows = Application.Current.Windows.Cast<Window>().Where(window => window.Visibility != Visibility.Hidden).ToList();
                foreach (Window window in windows)
                {
                    window.Close();
                }
                return;
            }

            MessageBox.Show("Termin otkazan!");
        }

        private void AddAppointment_OnClick(object sender, RoutedEventArgs e)
        {
            ChooseTypeOfAppoitnmentPageView chooseTypeOfAppoitnmentPageView = new ChooseTypeOfAppoitnmentPageView();
            this.NavigationService.Navigate(chooseTypeOfAppoitnmentPageView);
        }
    }
}
