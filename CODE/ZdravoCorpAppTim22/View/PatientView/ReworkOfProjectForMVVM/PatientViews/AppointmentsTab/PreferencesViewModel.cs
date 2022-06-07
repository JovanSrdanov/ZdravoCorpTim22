using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class PreferencesViewModel : ViewModel
    {
        public AppointmentDoctorViewModel Doctor { get; set; }
        public AppointmentRoomViewModel Room { get; set; }
        public AppointmentPatientViewModel Patient { get; set; }
        public DateTime SelectedDateTime { get; set; }
        public AppointmentType EnteredAppointmentType { get; set; }
        public string EnteredPriority { get; set; }
        public DateTime Tomorrow => DateTime.Now.AddDays(1);

        public ObservableCollection<AppointmentDoctorViewModel> DoctorsByType
        { get; set; }

        public PreferencesViewModel()
        {
            Patient P = (Patient)AuthenticationController.Instance.GetLoggedUser();
            Patient = new AppointmentPatientViewModel(P.Id, P.Name, P.Surname);

        }

        public void SetDoctorsByType()
        {
            DoctorsByType = new ObservableCollection<AppointmentDoctorViewModel>();

            List<Doctor> doctors = DoctorController.Instance.GetAll();

            foreach (Doctor doctor in doctors)
            {
                if (EnteredAppointmentType == AppointmentType.Operation)
                {
                    if (doctor.DoctorSpecialization.Name != "Regular")
                    {
                        DoctorsByType.Add(new AppointmentDoctorViewModel(doctor.Id, doctor.Name, doctor.Surname,
                            doctor.DoctorSpecialization.Name));
                    }
                }
                else
                {
                    DoctorsByType.Add(new AppointmentDoctorViewModel(doctor.Id, doctor.Name, doctor.Surname,
                        doctor.DoctorSpecialization.Name));
                }
            }


        }
    }
}
