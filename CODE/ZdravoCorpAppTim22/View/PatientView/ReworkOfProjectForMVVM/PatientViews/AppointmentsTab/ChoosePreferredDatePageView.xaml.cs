using System;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
 
    public partial class ChoosePreferredDatePageView : Page
    {

        public PreferencesViewModel _preferencesViewModel { get; set; }
        public ChoosePreferredDatePageView(PreferencesViewModel preferencesViewModel)
        {
            InitializeComponent();
            _preferencesViewModel = preferencesViewModel;
            datePicker.SelectedDate = DateTime.Now.AddDays(1).Date;
            datePicker.DisplayDateStart = DateTime.Now.AddDays(1).Date;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void AppointmentNext_OnClick(object sender, RoutedEventArgs e)
        {
            _preferencesViewModel.SelectedDateTime = (DateTime)datePicker.SelectedDate;
            NavigationService.Navigate(new ChoosePriorityPageView(_preferencesViewModel));
        }
    }
}
