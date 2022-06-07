using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows;
using Controller;
using Model;
using MVVM1;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class ChooseAppointmentFromSuggestedPageViewModel:ViewModel
    {
        private void ConvertToViewModel(PreferencesViewModel preferencesViewModel)
        {
            List<MedicalAppointment> medicalAppointments =
                MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(preferencesViewModel.Patient.Id,
                    preferencesViewModel.SelectedDateTime, preferencesViewModel.EnteredAppointmentType,
                    preferencesViewModel.EnteredPriority, preferencesViewModel.Doctor.Id);
            MedicalAppointmentsViewModels = new ObservableCollection<MedicalAppointmentsViewModel>();
            foreach (MedicalAppointment medicalAppointment in medicalAppointments)
            {
                MedicalAppointmentsViewModels.Add(new MedicalAppointmentsViewModel(medicalAppointment.Id,
                    medicalAppointment.Type, medicalAppointment.Interval, new AppointmentRoomViewModel(medicalAppointment.room),
                    new AppointmentPatientViewModel(medicalAppointment.patient),
                    new AppointmentDoctorViewModel(medicalAppointment.doctor)));
            }
        }
        public ObservableCollection<MedicalAppointmentsViewModel> MedicalAppointmentsViewModels { get; set; }

        private MedicalAppointmentsViewModel selectedMedicalAppointmentsViewModel;
        public MyICommand MakeAppointmentCommand { get; set; }

        public ChooseAppointmentFromSuggestedPageViewModel(PreferencesViewModel preferencesViewModel)
        {
            ConvertToViewModel(preferencesViewModel);

            MakeAppointmentCommand = new MyICommand(MakeAppointment, IsMedicalAppointmentSelected);



        }

        public void MakeAppointment()
        {
            MedicalAppointmentController.Instance.MakeAppointment(SelectedMedicalAppointmentsViewModel.Patient.Id, SelectedMedicalAppointmentsViewModel.Doctor.Id, SelectedMedicalAppointmentsViewModel.Room.Id,SelectedMedicalAppointmentsViewModel.Interval, SelectedMedicalAppointmentsViewModel.Type);
        }


        public MedicalAppointmentsViewModel SelectedMedicalAppointmentsViewModel
        {
            get { return selectedMedicalAppointmentsViewModel; }
            set
            {
                selectedMedicalAppointmentsViewModel = value;
                MakeAppointmentCommand.RaiseCanExecuteChanged();
            }
        }

        private bool IsMedicalAppointmentSelected()
        {
            return SelectedMedicalAppointmentsViewModel != null;
        }


    }
}