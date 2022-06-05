using System;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{

    public partial class ChoosingDateView : Page
    {
        public MedicalAppointmentsViewModel mavm { get; set; }
        public ChoosingDateView(MedicalAppointmentsViewModel medicalAppointmentsViewModel)
        {
            InitializeComponent();
            this.DataContext = medicalAppointmentsViewModel;
            mavm = medicalAppointmentsViewModel;
            DatePickerChangeAppoinment.SelectedDate = medicalAppointmentsViewModel.Interval.Start.Date;
        }

        private void ChooseChangeAppointment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DateTime selectedDate = (DateTime)DatePickerChangeAppoinment.SelectedDate;
            ChoosingIntervalsViewModel choosingIntervalsViewModel = new ChoosingIntervalsViewModel(mavm.Id, selectedDate);
            ChoosingIntervalsView choosingIntervalsView = new ChoosingIntervalsView(choosingIntervalsViewModel);
            this.NavigationService.Navigate(choosingIntervalsView);

        }

        private void Cancle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
