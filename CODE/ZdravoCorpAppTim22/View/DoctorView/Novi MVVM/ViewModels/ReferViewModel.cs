using Controller;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class ReferViewModel : ViewModel
    {
        //POLJA
        private NavigationService NavigationService;

        //PROPERTY
        public Patient SelectedPatient { get; set; }
        public string AnamnesisText { get; set; }
        public bool IsReferButtonEnabled { get; set; }
        public ObservableCollection<DoctorViewModel> DoctorList { get; set; }
        public ObservableCollection<AppointmentType> AppointmentTypeList { get; set; }

        private DateTime referalDate;
        public DateTime ReferalDate
        {
            get { return referalDate; }
            set
            {
                referalDate = value;
                isInputDataValid();
            }
        }

        private string referalTime;
        public string ReferalTime
        {
            get { return referalTime; }
            set
            {
                referalTime = value;
                isInputDataValid();

            }
        }

        public DateTime ReferalDateTime { get; set; }

        public bool IsDateTimeValid { get; set; }

        private AppointmentType selectedAppointmentType;
        public AppointmentType SelectedAppointmentType
        {
            get { return selectedAppointmentType; }
            set
            {
                selectedAppointmentType = value;
                setChecBoxVisibility();
                OnPropertyChanged("IsCheckBoxEnabled");
            }
        }

        private DoctorViewModel selectedDoctorViewModel;
        public DoctorViewModel SelectedDoctorViewModel
        {
            get { return selectedDoctorViewModel; }
            set
            {
                selectedDoctorViewModel = value;
                AppointmentTypeList = new ObservableCollection<AppointmentType>(GetAppropriateSpicializationAppointmentTypes());
                OnPropertyChanged("AppointmentTypeList");
            }
        }

        public bool IsCheckBoxEnabled { get; set; }
        public string Comment { get; set; }

        //KONSTRUKTOR
        public ReferViewModel(NavigationService navigationService, Patient selectedPatient)
        {
            NavigationService = navigationService;
            SelectedPatient = selectedPatient;
            DoctorList = new ObservableCollection<DoctorViewModel>(GetAvailableDoctorsForRefferal());
            SelectedDoctorViewModel = DoctorList[0];
            AppointmentTypeList = new ObservableCollection<AppointmentType>(GetAppropriateSpicializationAppointmentTypes());
            SelectedAppointmentType = AppointmentTypeList[0];
            ReferalDate = DateTime.Now;
            ReferalTime = "10:00";

            ConfirmCommand = new MyICommand(ExecuteConfirmCommand, CanExecuteConfirmCommand);
            CancelCommand = new MyICommand(ExecuteCancelCommand);
        }

        //POMOCNE FUNKCIJE
        private void isInputDataValid()
        {
#pragma warning disable CS0168
            try
            {
                if (ReferalTime == null)
                {
                    IsDateTimeValid = false;
                }
                else
                {
                    ReferalDateTime = DateTime.Parse(ReferalDate.ToShortDateString() + " " + ReferalTime);
                    IsDateTimeValid = true;
                }
                if (ConfirmCommand != null) ConfirmCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                IsDateTimeValid = false;
                if (ConfirmCommand != null) ConfirmCommand.RaiseCanExecuteChanged();
            }

#pragma warning restore CS0168 
        }

        private ObservableCollection<DoctorViewModel> GetAvailableDoctorsForRefferal()
        {
            Doctor selectedDoctor = AuthenticationController.Instance.GetLoggedUser() as Doctor;
            ObservableCollection<Doctor> allDoctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            allDoctors.Remove(selectedDoctor);

            ObservableCollection<DoctorViewModel> tempList = new ObservableCollection<DoctorViewModel>();
            foreach (var item in allDoctors)
            {
                tempList.Add(new DoctorViewModel(item));
            }

            return tempList;
        }

        private List<AppointmentType> GetAppropriateSpicializationAppointmentTypes()
        {
            List<AppointmentType> appointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            if (DoctorController.Instance.isDoctorRegular(SelectedDoctorViewModel.Doctor))
            {
                appointmentTypes.Remove(global::Model.AppointmentType.Operation);
            }
            return appointmentTypes;
        }
        private void setChecBoxVisibility()
        {
            if (SelectedAppointmentType != AppointmentType.Operation)
            {
                IsCheckBoxEnabled = false;
            }
            else
                IsCheckBoxEnabled = true;
        }
        private void updateAnamnesis()
        {

            Doctor refferedDoctor = SelectedDoctorViewModel.Doctor;
            string referralComment = "\n\nReffered to: " + refferedDoctor.Name + " " + refferedDoctor.Surname +
                "\nAppointment date: " + ReferalDateTime + "\nComment for the specialist:\n" + Comment;
            CreateReportViewModel.AnamnesisText += referralComment;
            RecordViewModel.createReportPage.AnamnesisBox.Text += referralComment;
        }

        private void createForwardedAppointment()
        {
            MedicalAppointment forwardedAppointment =
                new MedicalAppointment(-1, SelectedAppointmentType,
                getInterval(ReferalDateTime), null,
                SelectedPatient, SelectedDoctorViewModel.Doctor);
            forwardedAppointment.isUrgent = true;
            MedicalAppointmentController.Instance.Create(forwardedAppointment);
            RecordViewModel.newlyCreatedAppointments.Add(new MedicalAppointmentViewModel(forwardedAppointment));
        }

        private Interval getInterval(DateTime forwardedDate)
        {
            Interval forvardedAppointmentTimeLength = new Interval();
            forvardedAppointmentTimeLength.Start = forwardedDate;
            forvardedAppointmentTimeLength.End = forwardedDate;
            return forvardedAppointmentTimeLength;
        }

        //KOMANDE
        public MyICommand ConfirmCommand { get; set; }
        private void ExecuteConfirmCommand()
        {
            updateAnamnesis();
            createForwardedAppointment();
            CreateReportViewModel.IsReferButtonEnabled = false;
            RecordViewModel.createReportPage.ReferToSpecialistBtn.IsEnabled = false;
            this.NavigationService.GoBack();
        }
        private bool CanExecuteConfirmCommand()
        {
            return IsDateTimeValid;
        }

        public MyICommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            this.NavigationService.GoBack();
        }
    }
}
