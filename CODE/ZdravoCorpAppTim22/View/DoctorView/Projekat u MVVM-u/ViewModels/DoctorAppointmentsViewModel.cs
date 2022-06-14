using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.DoctorView.Commands;
using ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.Views;
using ZdravoCorpAppTim22.View.DoctorView.Services;

namespace ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels
{
    public class DoctorAppointmentsViewModel : ViewModel
    {
        private ObservableCollection<MedicalAppointmentViewModel> CurDocAppointmentsObservable { get; set; }
        private MedicalAppointmentViewModel SelectedItem { get; set; }
        private readonly Doctor _loggedInDoctor;

        public DoctorAppointmentsViewModel(NavService doctorAppointmentsNavigationService)
        {
            CurDocAppointmentsObservable = new ObservableCollection<MedicalAppointmentViewModel>();
            _loggedInDoctor = (Doctor)AuthenticationController.Instance.GetLoggedUser();
            var allDoctorAppointments = _loggedInDoctor.MedicalAppointment;
            CurDocAppointmentsObservable = new ObservableCollection<MedicalAppointmentViewModel>();

            BackCommand = new NavigateCommand(doctorAppointmentsNavigationService);
            //CreateAppointmentCommand = new NavigateCommand(new 
                //NavService(DoctorMainWindow._navigationBase, CreateCreateAppointmentViewModel));
        }

        public ICommand CreateAppointmentCommand { get; }
        public ICommand BeginAppointmentCommand { get; }
        public ICommand CancelAppointmentCommand { get; }
        public ICommand ViewRecordCommand { get; }
        public ICommand BackCommand { get; }

        private CreateAppointmentViewModel CreateCreateAppointmentViewModel()
        {
            return new CreateAppointmentViewModel(new 
                NavService(DoctorMainWindow._navigationBase, CreateDoctorAppointmentsViewModel));
        }

        private DoctorAppointmentsViewModel CreateDoctorAppointmentsViewModel()
        {
            return new DoctorAppointmentsViewModel(new 
                NavService(DoctorMainWindow._navigationBase, CreateDoctorAppointmentsViewModel));
        }
    }
}
