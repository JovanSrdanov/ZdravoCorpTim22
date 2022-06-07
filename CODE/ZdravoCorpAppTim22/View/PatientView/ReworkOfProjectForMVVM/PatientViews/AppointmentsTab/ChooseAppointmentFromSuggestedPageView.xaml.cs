using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{

    public partial class ChooseAppointmentFromSuggestedPageView : Page
    {
        public ChooseAppointmentFromSuggestedPageView(PreferencesViewModel preferencesViewModel)
        {
            InitializeComponent();



            ChooseAppointmentFromSuggestedPageViewModel chooseAppointmentFromSuggestedPageViewModel =
                new ChooseAppointmentFromSuggestedPageViewModel(preferencesViewModel);

            this.DataContext = chooseAppointmentFromSuggestedPageViewModel;

        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SuccessCreatingAppointment());
        }
    }
}
