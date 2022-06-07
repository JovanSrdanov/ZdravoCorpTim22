using Controller;
using Model;
using MVVM1;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class AppointmentsPageViewModel : ViewModel
    {
        private ObservableCollection<MedicalAppointmentsViewModel> medicalAppointments;

        private MedicalAppointmentsViewModel selectedMedicalAppointmentsViewModel;

        public MedicalAppointmentsViewModel SelectedMedicalAppointmentsViewModel
        {
            get { return selectedMedicalAppointmentsViewModel; }
            set
            {
                selectedMedicalAppointmentsViewModel = value;
                DeleteAppointmentCommand.RaiseCanExecuteChanged();
                ChangeAppointmentCommand.RaiseCanExecuteChanged();
            }
        }

        private bool IsMedicalAppointmentSelected()
        {
            return SelectedMedicalAppointmentsViewModel != null;
        }
        public void DeleteAppointment()
        {
            MedicalAppointmentController.Instance.DeleteByID(SelectedMedicalAppointmentsViewModel.Id);
            MedicalAppointments.Remove(selectedMedicalAppointmentsViewModel);
        }


        public MyICommand DeleteAppointmentCommand { get; set; }
        public MyICommand ChangeAppointmentCommand { get; set; }

        public ObservableCollection<MedicalAppointmentsViewModel> MedicalAppointments
        {
            get { return medicalAppointments; }
            set
            {
                medicalAppointments = value;
                OnPropertyChanged();
            }
        }

        public AppointmentsPageViewModel()
        {
            ChangeAppointmentCommand = new MyICommand(null, IsMedicalAppointmentSelected);

            DeleteAppointmentCommand = new MyICommand(DeleteAppointment, IsMedicalAppointmentSelected);

            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            MedicalAppointments = ConvertMedicalAppoitnmentsToVM(patient.medicalAppointment);

        }

        private ObservableCollection<MedicalAppointmentsViewModel> ConvertMedicalAppoitnmentsToVM(List<MedicalAppointment> medicalAppointments)
        {
            ObservableCollection<MedicalAppointmentsViewModel> medicalAppointmentsViewModels =
                new ObservableCollection<MedicalAppointmentsViewModel>();

            if (medicalAppointments != null)
                foreach (MedicalAppointment medicalAppointment in medicalAppointments)
                {
                    AppointmentDoctorViewModel appointmentDoctorViewModel =
                        new AppointmentDoctorViewModel(medicalAppointment.doctor.Id, medicalAppointment.doctor.Name,
                            medicalAppointment.doctor.Surname, medicalAppointment.doctor.DoctorSpecialization.Name);

                    AppointmentRoomViewModel appointmentRoomViewModel =
                        new AppointmentRoomViewModel(medicalAppointment.room.Id, medicalAppointment.room.Level, medicalAppointment.room.Name);


                    AppointmentPatientViewModel appointmentPatientViewModel =
                        new AppointmentPatientViewModel(medicalAppointment.patient.Id, medicalAppointment.patient.Name,
                            medicalAppointment.patient.Surname);

                    MedicalAppointmentsViewModel medicalAppointmentsViewModel =
                        new MedicalAppointmentsViewModel(medicalAppointment.Id, medicalAppointment.Type,
                            medicalAppointment.Interval, appointmentRoomViewModel, appointmentPatientViewModel,
                            appointmentDoctorViewModel);

                    medicalAppointmentsViewModels.Add(medicalAppointmentsViewModel);

                }


            return medicalAppointmentsViewModels;
        }
    }
}