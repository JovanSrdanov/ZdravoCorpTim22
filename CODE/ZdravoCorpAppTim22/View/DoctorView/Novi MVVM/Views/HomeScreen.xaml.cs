using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
        private HomeScreenViewModel viewModel;
        public static HomeScreen doctorHomeScreen;
        public static Doctor LoggedInDoctor { get; set; }
        public HomeScreen()
        {
            viewModel = new HomeScreenViewModel();
            InitializeComponent();
            this.DataContext = viewModel;

            doctorHomeScreen = this;
        }
        private void ScheduleListBtn(object sender, RoutedEventArgs e)
        {
            ListAppointmentViewModel viewModel = new ListAppointmentViewModel(this.NavigationService);
            ListAppointments listAppointments = new ListAppointments(viewModel);
            this.NavigationService.Navigate(listAppointments);
        }

        private void ViewMedRecordsClick(object sender, RoutedEventArgs e)
        {
            ListMedicalRecordViewModel viewModel = new ListMedicalRecordViewModel(this.NavigationService);
            ListMedicalRecordPage listMedicalRecordPage = new ListMedicalRecordPage(viewModel);
            this.NavigationService.Navigate(listMedicalRecordPage);
        }

        private void RequestForAbsenceBtnClick(object sender, RoutedEventArgs e)
        {
            RequestForAbsenceViewModel viewModel = new RequestForAbsenceViewModel(this.NavigationService);
            RequestForAbsence requestForAbsence = new RequestForAbsence(viewModel);
            this.NavigationService.Navigate(requestForAbsence);

        }

        private void DrugsBtnClick(object sender, RoutedEventArgs e)
        {
            DrugsPageViewModel viewModel = new DrugsPageViewModel(this.NavigationService);
            DrugsPage requestDrugs = new DrugsPage(viewModel);
            this.NavigationService.Navigate(requestDrugs);
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }
    }
}
