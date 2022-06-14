using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RequestForAbsenceViewModel : ViewModel
    {
        //DEMO
        public static bool IsRepeating { get; set; }
        public static DockPanel MainWindowContent { get; set; }


        //POLJA
        private NavigationService navigationService;
        private Doctor requestingDoctor;

        //PROPERTY
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }

        private string reasonForAvsence;
        public string ReasonForAbsence
        {
            get { return reasonForAvsence; }
            set 
            {
                reasonForAvsence = value;
                OnPropertyChanged(ReasonForAbsence);
            }
        }
        public bool IsUrgent { get; set; }

        //KONSTRUKTOR
        public RequestForAbsenceViewModel(NavigationService navigationService)
        {
            //DEMO
            MainWindowContent = (DockPanel)MainDoctorWindow.MainDoctorWindowInstance.Content;

            this.navigationService = navigationService;
            requestingDoctor = (Doctor)AuthenticationController.Instance.GetLoggedUser();
            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;

            CreateRequestCommand = new MyICommand(ExecuteCreateRquest);
            CancelCommand = new MyICommand(ExcecuteCancel);
            ViewRequestListCommand = new MyICommand(ExecuteViewRequestList);

            //DEMO
            DemoCommand = new MyICommand(ExecuteDemoCommand);
            StopCommand = new MyICommand(ExecuteStopCommand);
        }

        //FUNKCIJE
        private bool validateDate(Interval absenceInterval)
        {
            bool returnValue = true;
            if (!RequestForAbsenceController.Instance.validateDate(absenceInterval))
            {
                MessageBox.Show("Invalid date input", "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }
        private bool hasAlreadyRequestedAbsenceInSelectedPeriod(Interval absenceInterval, Doctor requestingDoctor)
        {
            bool returnValue = false;
            if (RequestForAbsenceController.Instance.hasAlreadyRequestedAbsenceInSelectedPeriod(absenceInterval, requestingDoctor))
            {
                MessageBox.Show("You have already requested for absence in the selected time period",
                    "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }
        private bool alreadyHasAnAppointment(Interval absenceInterval, Doctor requestingDoctor)
        {
            bool returnValue = false;
            if (RequestForAbsenceController.Instance.alreadyHasAnAppointment(absenceInterval, requestingDoctor))
            {
                MessageBox.Show("You have an appointment at that time", "Request for absence",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }
        private bool areMultipleDoctorsOfSameTypeOnLeave(bool isUrgent, Interval absenceInterval)
        {
            bool returnValue = false;
            if (isUrgent == false && RequestForAbsenceController.Instance.
                areMultipleDoctorsOfSameTypeOnLeave(requestingDoctor.DoctorSpecialization.Name, absenceInterval))
            {
                MessageBox.Show("There are already multiple doctors of the same specialization on leave for that time period",
                    "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }
        private Interval getInterval()
        {
            Interval interval = new Interval();
            interval.Start = SelectedStartDate;
            interval.End = SelectedEndDate;
            return interval;
        }

        //KOMANDE
        public MyICommand CreateRequestCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public MyICommand ViewRequestListCommand { get; set; }

        private void ExecuteCreateRquest()
        {
            Interval absenceInterval = getInterval();
            if (!validateDate(absenceInterval)) return;
            if (hasAlreadyRequestedAbsenceInSelectedPeriod(absenceInterval, requestingDoctor)) return;
            if (alreadyHasAnAppointment(absenceInterval, requestingDoctor)) return;
            if (areMultipleDoctorsOfSameTypeOnLeave(IsUrgent, absenceInterval)) return;

            Model.RequestForAbsence newRequest = new Model.RequestForAbsence
                (ReasonForAbsence, IsUrgent, absenceInterval, requestingDoctor);
            RequestForAbsenceController.Instance.Create(newRequest);

            MessageBox.Show("Request successfuly created",
                    "Request for absence", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExcecuteCancel()
        {
            this.navigationService.Navigate(new Uri (
                "/View/DoctorView/Novi MVVM/Views/HomeScreen.xaml", UriKind.Relative));
        }

        private void ExecuteViewRequestList()
        {
            RequestListViewModel requestListViewModel = new RequestListViewModel(navigationService);
            RequestList requestList = new RequestList(requestListViewModel);
            this.navigationService.Navigate(requestList);
        }

        //DEMO
        public MyICommand DemoCommand { get; set; }
        private async void ExecuteDemoCommand()
        {
            IsRepeating = true;
            RequestForAbsenceViewModel testViewModel = new RequestForAbsenceViewModel(navigationService);
            RequestForAbsence testPage = new RequestForAbsence(testViewModel);
            await Task.Delay(1000);
            MainDoctorWindow.MainDoctorWindowInstance.Content = testPage;

            while (IsRepeating)
            {
                //START DATE
                testPage.AbsenceStartDatePicker.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testPage.AbsenceStartDatePicker.SelectedDate = DateTime.Now.AddDays(3);
                testPage.AbsenceStartDatePicker.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //END DATE
                testPage.AbsenceEndDatePicker.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testPage.AbsenceEndDatePicker.SelectedDate = DateTime.Now.AddDays(6);
                testPage.AbsenceEndDatePicker.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //REASON FOR ABSENCE
                testPage.ReasonForAbsenceTextBox.Focus();
                testPage.ReasonForAbsenceTextBox.Text = "DEMO TEXT";
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //CHECKBOX
                testPage.UrgentCheckBox.IsChecked = true;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //PUT RED
                testPage.SendBtn.Background = Brushes.Red;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //CLEAR DATA
                testPage.SendBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8C39A5"));
                testPage.ReasonForAbsenceTextBox.Text = "";
                testPage.AbsenceStartDatePicker.SelectedDate = null;
                testPage.AbsenceEndDatePicker.SelectedDate = null;
                testPage.UrgentCheckBox.IsChecked = false;
                if (IsRepeating == false)
                {
                    MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
            }
        }

        public MyICommand StopCommand { get; set; }
        private void ExecuteStopCommand()
        {
            IsRepeating = false;
            MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
        }
    }
}
