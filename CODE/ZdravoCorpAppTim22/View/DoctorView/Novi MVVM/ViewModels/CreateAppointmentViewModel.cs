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
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class CreateAppointmentViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;
        private readonly Doctor _loggedInDoctor;

        //PROPERTY
        public ObservableCollection<AppointmentTypeViewModel> AppointmentsTypeList { get; set; }
        public ObservableCollection<PatientViewModel> PatientList { get; set; }
        public ObservableCollection<RoomViewModel> RoomList { get; set; }

        public bool IsDateTimeValid { get; set; }

        //SELECTED APPOINTMENT TYPE
        private AppointmentTypeViewModel selectedAppointmentType;
        public AppointmentTypeViewModel SelectedAppointmentType
        {
            get { return selectedAppointmentType; }
            set
            {
                selectedAppointmentType = value;
                OnPropertyChanged("SelectedAppointmentType");
                ConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        //SELECTED PATIENT
        private PatientViewModel selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
                ConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        //SELECTED ROOM
        private RoomViewModel selectedRoom;
        public RoomViewModel SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
                ConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        //DATETIME
        public DateTime StartDateTime { get; set; }
        private string selectedTime;
        public string SelectedTime
        {
            get { return selectedTime; }
            set
            {
                selectedTime = value;
                OnPropertyChanged("SelectedTime");
                IsDateValid(SelectedDate, selectedTime);
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
                IsDateValid(selectedDate, SelectedTime);
            }
        }
        private void IsDateValid(DateTime startDate, string startTime)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                if (startTime == null)
                {
                    IsDateTimeValid = false;
                }
                else
                {
                    StartDateTime = DateTime.Parse(startDate.ToShortDateString() + " " + startTime);
                    IsDateTimeValid = true;
                }
                
                ConfirmCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                IsDateTimeValid = false;
                ConfirmCommand.RaiseCanExecuteChanged();
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        //KONSTRUKTOR                                                                                   //DODAO
        public CreateAppointmentViewModel(System.Windows.Navigation.NavigationService navigationService)
        {
            this.navigationService = navigationService;

            _loggedInDoctor = AuthenticationController.Instance.GetLoggedUser() as Doctor;
            AppointmentsTypeList = new ObservableCollection<AppointmentTypeViewModel>(GetAppropriateAppointmentType());
            PatientList = new ObservableCollection<PatientViewModel>(GetPatientList());
            RoomList = new ObservableCollection<RoomViewModel>(GetRoomList());

            ConfirmCommand = new MyICommand(ExecuteConfirmCommand, CanExecuteConfirmCommand);
            CancelCommand = new MyICommand(ExecuteCancelCommand);

            if (AppointmentsTypeList.Count() > 0) SelectedAppointmentType = AppointmentsTypeList[0];
            if (PatientList.Count() > 0) SelectedPatient = PatientList[0];
            if (RoomList.Count() > 0) selectedRoom = RoomList[0];
            SelectedDate = DateTime.Now;

            //DEMO
            StopDemoCommand = new MyICommand(ExecuteStopDemoCommand);
        }

        private List<AppointmentTypeViewModel> GetAppropriateAppointmentType()
        {
            List<AppointmentTypeViewModel> tempList = new List<AppointmentTypeViewModel>();
            foreach (AppointmentType item in Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList())
            {
                tempList.Add(new AppointmentTypeViewModel(item));
            }
            if (DoctorController.Instance.isDoctorRegular(_loggedInDoctor))
            {
                foreach (var item in tempList)
                {
                    if (item.AppointmentType == AppointmentType.Operation)
                    {
                        tempList.Remove(item);
                        break;
                    }
                }
            }

            return tempList;
        }

        private List<PatientViewModel> GetPatientList()
        {
            List<PatientViewModel> tempList = new List<PatientViewModel>();
            foreach (var patient in PatientController.Instance.GetAll())
            {
                tempList.Add(new PatientViewModel(patient));
            }
            return tempList;
        }
        private List<RoomViewModel> GetRoomList()
        {
            List<RoomViewModel> tempList = new List<RoomViewModel>();
            foreach (var room in RoomController.Instance.GetAll())
            {
                tempList.Add(new RoomViewModel(room));
            }
            return tempList;
        }

        //POMOCNE FUNKCIJE

        private Interval getInterval(DateTime startDateTime)
        {
            Interval interval = new Interval();
            interval.Start = startDateTime;
            interval.End = startDateTime;
            return interval;
        }

        //KOMANDE
        public MyICommand CancelCommand { get; set; }
        public MyICommand ConfirmCommand { get; set; }

        private void ExecuteConfirmCommand()
        {
            Interval interval = getInterval(StartDateTime);
            MedicalAppointment newMedicalAppointment = new MedicalAppointment
                (-1, SelectedAppointmentType.AppointmentType, interval, SelectedRoom.Room, SelectedPatient.Patient, _loggedInDoctor);
            MedicalAppointmentController.Instance.Create(newMedicalAppointment);
            MedicalAppointmentViewModel newMedicalAppointmentViewModel = new MedicalAppointmentViewModel(newMedicalAppointment);
            ListAppointmentViewModel.CurDocAppointmentsObservable.Add(newMedicalAppointmentViewModel);

            this.navigationService.GoBack();
        }

        private bool CanExecuteConfirmCommand()
        {
            return IsDateTimeValid;
        }

        private void ExecuteCancelCommand()
        {
            this.navigationService.GoBack();
        }

        //DEMO

        public MyICommand StopDemoCommand { get; set; }
        private void ExecuteStopDemoCommand()
        {
            ListAppointmentViewModel.IsRepeating = false;
            MainDoctorWindow.MainDoctorWindowInstance.Content = ListAppointmentViewModel.MainWindowContent;
        }
    }
}
