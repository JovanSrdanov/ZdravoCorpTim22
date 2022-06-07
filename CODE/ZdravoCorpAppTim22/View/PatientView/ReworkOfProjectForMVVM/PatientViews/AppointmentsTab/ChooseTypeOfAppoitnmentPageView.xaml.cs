using Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{

    public partial class ChooseTypeOfAppoitnmentPageView : Page
    {
        public ChooseTypeOfAppoitnmentPageView()
        {
            InitializeComponent();
            ChooseAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void AppointmentNext_OnClick(object sender, RoutedEventArgs e)
        {

            PreferencesViewModel preferencesViewModel = new PreferencesViewModel();
            preferencesViewModel.EnteredAppointmentType = (AppointmentType)ChooseAppointmentType.SelectedItem;

            ChoosePreferredDoctorPageView choosePreferredDoctorPageView = new ChoosePreferredDoctorPageView(preferencesViewModel);

            this.NavigationService.Navigate(choosePreferredDoctorPageView);
        }
    }
}
