using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
   
    public partial class ChoosePreferredDoctorPageView : Page
    {
        public PreferencesViewModel _preferencesViewModel { get; set; }

        public ChoosePreferredDoctorPageView(PreferencesViewModel preferencesViewModel)
        {
            InitializeComponent();
            _preferencesViewModel = preferencesViewModel;
            _preferencesViewModel.SetDoctorsByType();
            this.DataContext = _preferencesViewModel;

        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void AppointmentNext_Click(object sender, RoutedEventArgs e)
        {

            _preferencesViewModel.Doctor = (AppointmentDoctorViewModel)ChooseDoctor.SelectedItem;
            ChoosePreferredDatePageView choosePreferredDatePageView = new ChoosePreferredDatePageView(_preferencesViewModel);
            this.NavigationService.Navigate(choosePreferredDatePageView);
        }
    }
}
