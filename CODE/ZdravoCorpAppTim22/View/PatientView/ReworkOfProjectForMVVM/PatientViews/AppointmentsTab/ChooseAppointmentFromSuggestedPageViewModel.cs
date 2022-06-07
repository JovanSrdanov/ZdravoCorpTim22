using Controller;
using Model;
using MVVM1;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class ChooseAppointmentFromSuggestedPageViewModel : ViewModel
    {
        private void ConvertToViewModel(PreferencesViewModel preferencesViewModel)
        {
            List<MedicalAppointmentDTOforSuggestions> medicalAppointments =
                MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(preferencesViewModel.Patient.Id,
                    preferencesViewModel.SelectedDateTime, preferencesViewModel.EnteredAppointmentType,
                    preferencesViewModel.EnteredPriority, preferencesViewModel.Doctor.Id);
            MedicalAppointmentsViewModels = new ObservableCollection<MedicalAppointmentsViewModel>();
            foreach (MedicalAppointmentDTOforSuggestions medicalAppointment in medicalAppointments)
            {
                MedicalAppointmentsViewModels.Add(new MedicalAppointmentsViewModel(medicalAppointment.Id,
                    medicalAppointment.Type, medicalAppointment.Interval, new AppointmentRoomViewModel(medicalAppointment.Room),
                    new AppointmentPatientViewModel(medicalAppointment.Patient),
                    new AppointmentDoctorViewModel(medicalAppointment.Doctor)));
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
            MedicalAppointmentController.Instance.MakeAppointment( SelectedMedicalAppointmentsViewModel.Doctor.Id, SelectedMedicalAppointmentsViewModel.Room.Id, SelectedMedicalAppointmentsViewModel.Interval, SelectedMedicalAppointmentsViewModel.Type);




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