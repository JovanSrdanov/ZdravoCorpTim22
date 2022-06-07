using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public partial class ChoosePriorityPageView : Page
    {
        public PreferencesViewModel _preferencesViewModel { get; set; }
        public ChoosePriorityPageView(PreferencesViewModel preferencesViewModel)
        {
            InitializeComponent();
            _preferencesViewModel = preferencesViewModel;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            
        }

        private void AppointmentNext_OnClick(object sender, RoutedEventArgs e)
        {
            _preferencesViewModel.EnteredPriority = (bool)TimeRadioButton.IsChecked ? "Vreme" : "Lekar";
            NavigationService.Navigate(new ChooseAppointmentFromSuggestedPageView(_preferencesViewModel));
        }
    }
}
