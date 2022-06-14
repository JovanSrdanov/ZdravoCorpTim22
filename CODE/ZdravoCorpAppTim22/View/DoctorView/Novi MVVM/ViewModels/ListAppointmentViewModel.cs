using Controller;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class ListAppointmentViewModel : ViewModel
    {
        //DEMO
        public static bool IsRepeating { get; set; }
        public static DockPanel MainWindowContent { get; set; }


        public static ObservableCollection<MedicalAppointmentViewModel> CurDocAppointmentsObservable { get; set; }

        //Za obradu selected itema za delete
        private MedicalAppointmentViewModel selectedMedicalAppointment;
        public MedicalAppointmentViewModel SelectedMedicalAppointment
        {
            get { return selectedMedicalAppointment; }
            set
            {
                selectedMedicalAppointment = value;
                if (value != null)
                {
                    SelectedMedAppBackup = value;
                }
                CancelAppointmentCommand.RaiseCanExecuteChanged();
                ViewRecordCommand.RaiseCanExecuteChanged();
                BeginAppointmentCommand.RaiseCanExecuteChanged();
            }
        }

        public static MedicalAppointmentViewModel MedAppBackup { get; set; }

        public MedicalAppointmentViewModel SelectedMedAppBackup { get; set; }

        //Za obradu selected itema za delete

        private readonly Doctor _loggedInDoctor;

        public NavigationService navigationService;

        //KONSTRUKTOR
        public ListAppointmentViewModel(NavigationService navService)
        {
            //DEMO
            MainWindowContent = (DockPanel)MainDoctorWindow.MainDoctorWindowInstance.Content;

            navigationService = navService;
            _loggedInDoctor = (Doctor)AuthenticationController.Instance.GetLoggedUser();
            CurDocAppointmentsObservable = new ObservableCollection<MedicalAppointmentViewModel>(getMedicalAppointmentObservable());

            ViewRecordCommand = new MyICommand(ExecuteViewRecordCommand, CanExecuteViewRecordCommand);
            CreateAppointmentCommand = new MyICommand(ExecuteCreateAppointmentCommand);
            BeginAppointmentCommand = new MyICommand(ExecuteBeginAppointmentCommand, CanExecuteBeginAppointmentCommand);
            CancelAppointmentCommand = new MyICommand(ExcecuteDelete, CanExcecuteDelete);
            BackCommand = new MyICommand(ExcecuteBack);

            //DEMO
            DemoCommand = new MyICommand(ExecuteDemoCommand);
            StopCommand = new MyICommand(ExecuteStopCommand);
        }

        List<MedicalAppointmentViewModel> getMedicalAppointmentObservable()
        {
            var allDoctorAppointments = _loggedInDoctor.MedicalAppointment;
            List<MedicalAppointmentViewModel> allDoctorAppointmentsObservable = 
                new List<MedicalAppointmentViewModel>();
            foreach (MedicalAppointment temp in allDoctorAppointments)
            {
                allDoctorAppointmentsObservable.Add(new MedicalAppointmentViewModel(temp));
            }
            return allDoctorAppointmentsObservable;
        }

        //KOMANDE

        public MyICommand CreateAppointmentCommand { get; set; }
        public MyICommand BeginAppointmentCommand { get; set; }
        public MyICommand CancelAppointmentCommand { get; set; }
        public MyICommand ViewRecordCommand { get; set; }
        public MyICommand BackCommand { get; set; }

        private void ExecuteBeginAppointmentCommand()
        {
            MedAppBackup = SelectedMedicalAppointment;
            var viewModel = new RecordViewModel(1, SelectedMedicalAppointment.Patient, navigationService);
            var recordPage = new RecordPage(viewModel);
            this.navigationService.Navigate(recordPage);
        }

        private bool CanExecuteBeginAppointmentCommand()
        {
            return SelectedMedicalAppointment != null;
        }

        private void ExecuteCreateAppointmentCommand()
        {                                                                   //DODAO
            var viewModel = new CreateAppointmentViewModel(navigationService);
            var createAppointmentPage = new CreateAppointmentPage(viewModel);
            this.navigationService.Navigate(createAppointmentPage);
        }

        public void ExcecuteBack()
        {
            this.navigationService.Navigate(new Uri(
                "/View/DoctorView/Novi MVVM/Views/HomeScreen.xaml", UriKind.Relative));
        }

        public void ExcecuteDelete()
        {
            CurDocAppointmentsObservable.Remove(SelectedMedAppBackup);
            MedicalAppointmentController.Instance.DeleteByID(SelectedMedAppBackup.Id);
            SelectedMedicalAppointment = null;
            SelectedMedAppBackup = null;
        }
        public bool CanExcecuteDelete()
        {
            return SelectedMedicalAppointment != null;
        }

        private void ExecuteViewRecordCommand()
        {
            RecordViewModel viewModel = new RecordViewModel(-1, SelectedMedicalAppointment.Patient, navigationService);
            RecordPage recordPage = new RecordPage(viewModel);
            this.navigationService.Navigate(recordPage);
        }

        private bool CanExecuteViewRecordCommand()
        {
            if (SelectedMedicalAppointment == null)
            {
                return false;
            }
            else if (PatientController.Instance.GetByID(SelectedMedicalAppointment.Patient.Id).medicalRecord == null)
            {
                return false;
            }
            return true;
        }

        //DEMO

        public MyICommand DemoCommand { get; set; }
        private async void ExecuteDemoCommand()
        {
            IsRepeating = true;

            while (IsRepeating)
            { 
                CreateAppointmentViewModel testViewModel = new CreateAppointmentViewModel(navigationService);
                CreateAppointmentPage testPage = new CreateAppointmentPage(testViewModel);
                await Task.Delay(500);
                MainDoctorWindow.MainDoctorWindowInstance.Content = testPage;

                //APPOINTMENT
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testPage.AppointmentTypeCBOX.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                testViewModel.SelectedAppointmentType = testViewModel.AppointmentsTypeList[1];
                testPage.AppointmentTypeCBOX.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);


                //TIME
                testPage.TimeComboBox.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testViewModel.SelectedTime = "17:35";
                testPage.TimeComboBox.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);


                //DATE
                testPage.datePicker.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testViewModel.SelectedDate = DateTime.Now.AddDays(3);
                testPage.datePicker.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);


                //ROOM
                testPage.RoomComboBox.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testViewModel.SelectedRoom = testViewModel.RoomList[1];
                testPage.RoomComboBox.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);


                //PATIENT
                testPage.PatientComboBox.IsDropDownOpen = true;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
                testViewModel.SelectedPatient = testViewModel.PatientList[3];
                testPage.PatientComboBox.IsDropDownOpen = false;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);

                //MAKE APPOINTMENT
                Interval testInterval = new Interval();
                testInterval.Start = testViewModel.SelectedDate;
                testInterval.End = testViewModel.SelectedDate;

                MedicalAppointmentViewModel testMedAppViewModel = new MedicalAppointmentViewModel
                    (new MedicalAppointment(-1, testViewModel.SelectedAppointmentType.AppointmentType, testInterval, testViewModel.SelectedRoom.Room,
                    testViewModel.SelectedPatient.Patient, (AuthenticationController.Instance.GetLoggedUser() as Doctor)));

                testPage.confirmButton.Background = Brushes.Red;
                CurDocAppointmentsObservable.Add(testMedAppViewModel);

                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    CurDocAppointmentsObservable.Remove(testMedAppViewModel);
                    _loggedInDoctor.MedicalAppointment.Remove(testMedAppViewModel.MedicalAppointment);
                    break;
                }
                else await Task.Delay(2000);

                //CANCEL APPOINTMENT
                ListAppointments backPage = new ListAppointments(this);
                MainDoctorWindow.MainDoctorWindowInstance.Content = backPage;
                SelectedMedicalAppointment = testMedAppViewModel;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    CurDocAppointmentsObservable.Remove(testMedAppViewModel);
                    _loggedInDoctor.MedicalAppointment.Remove(testMedAppViewModel.MedicalAppointment);
                    break;
                }
                else await Task.Delay(2000);
                backPage.btnDelete.Background = Brushes.Red;
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    CurDocAppointmentsObservable.Remove(testMedAppViewModel);
                    _loggedInDoctor.MedicalAppointment.Remove(testMedAppViewModel.MedicalAppointment);
                    break;
                }
                else await Task.Delay(2000);
                CurDocAppointmentsObservable.Remove(testMedAppViewModel);
                _loggedInDoctor.MedicalAppointment.Remove(testMedAppViewModel.MedicalAppointment);
                if (IsRepeating == false)
                {
                    //MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
                    break;
                }
                else await Task.Delay(2000);
            }
        }

        public MyICommand StopCommand { get; set; }
        private void ExecuteStopCommand()
        {
            IsRepeating = false;
            //if (SelectedMedicalAppointment != null) CurDocAppointmentsObservable.Remove(SelectedMedicalAppointment);
            /*if (SelectedMedicalAppointment != null)
            {
                CurDocAppointmentsObservable.RemoveAt(CurDocAppointmentsObservable.Count() - 1);
                _loggedInDoctor.MedicalAppointment.RemoveAt(CurDocAppointmentsObservable.Count() - 1);
            }*/
            MainDoctorWindow.MainDoctorWindowInstance.Content = MainWindowContent;
        }
    }
}
