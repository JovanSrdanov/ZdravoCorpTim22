using Model;
using Service;
using System;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.PackedObjects;

namespace Controller
{
    public class MedicalAppointmentController : GenericController<MedicalAppointmentService, MedicalAppointment>
    {
        private static MedicalAppointmentController instance;
        private MedicalAppointmentController() : base(MedicalAppointmentService.Instance) { }
        public static MedicalAppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentController();
                }

                return instance;
            }
        }

        public ObservableCollection<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(Patient enteredPatient, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority, Doctor enteredDoctor)
        {
            EnteredPreferences enteredPreferences = new EnteredPreferences( enteredPatient,
                 enteredDateTime,  enteredAppointmentType,  enteredPriority,
                 enteredDoctor);

            return MedicalAppointmentService.Instance.GetSuggestedMedicalAppointments(enteredPreferences);
        }
        public ObservableCollection<MedicalAppointmentStruct> GetNewMedicalAppointments(Doctor doctor, Room room, Patient enteredPatient, DateTime selectedDateTime, AppointmentType type)
        {

            ForChangeMedicalAppointment forChangeMedicalAppointment =
                new ForChangeMedicalAppointment(doctor, room, enteredPatient, selectedDateTime, type);

            return MedicalAppointmentService.Instance.GetNewMedicalAppointments(forChangeMedicalAppointment);
        }








        /// ///////////////////// ///////////////////// ///////////////////// ///////////////////// //////////////////
        public ObservableCollection<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(AppointmentPreferences appointmentPreferences)
        {
            //luka
            return MedicalAppointmentService.Instance.GetSuggestedMedicalAppointments(appointmentPreferences);
        }


        public ObservableCollection<MedicalAppointment> GetUnavailableMedicalAppointmentsInNextHour(AppointmentPreferences appointmentPreferences)
        {
            //luka
            return MedicalAppointmentService.Instance.GetUnavailableMedicalAppointmentsInNextHour(appointmentPreferences);
        }


    }
}