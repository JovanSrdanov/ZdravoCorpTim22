using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.PackedObjects;
using ZdravoCorpAppTim22.Model.Utility;

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

        public List<MedicalAppointmentDTOforSuggestions> GetSuggestedMedicalAppointments(int PatiendID, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority, int DoctorID)
        {
           
            EnteredPreferences enteredPreferences = new EnteredPreferences(PatientController.Instance.GetByID(PatiendID), enteredDateTime, enteredAppointmentType, enteredPriority, DoctorController.Instance.GetByID(DoctorID));

             return MedicalAppointmentService.Instance.GetSuggestedMedicalAppointments(enteredPreferences);
            
        }
        public List<Interval> GetNewMedicalAppointments(int id, DateTime selecteDateTime)
        {
            MedicalAppointment medicalAppointment = MedicalAppointmentController.Instance.GetByID(id);

            ForChangeMedicalAppointment forChangeMedicalAppointment =
                new ForChangeMedicalAppointment(medicalAppointment.doctor, medicalAppointment.room, medicalAppointment.patient, selecteDateTime, medicalAppointment.Type);

            return MedicalAppointmentService.Instance.GetNewMedicalAppointments(forChangeMedicalAppointment);
        }

        public void MakeAppointment( int DoctorID, int RoomID, Interval interval, AppointmentType type)
        {
            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            Room room = RoomController.Instance.GetByID(RoomID);
            Doctor doctor = DoctorController.Instance.GetByID(DoctorID);
            MedicalAppointment medicalAppointment = new MedicalAppointment(-2,type,interval,room,patient,doctor);
            MedicalAppointmentService.Instance.MakeAppointment(medicalAppointment);

        }








        /// ///////////////////// ///////////////////// ///////////////////// ///////////////////// //////////////////
        public ObservableCollection<MedicalAppointmentDTOforSuggestions> GetSuggestedMedicalAppointments(AppointmentPreferences appointmentPreferences)
        {
            //luka
            return MedicalAppointmentService.Instance.GetSuggestedMedicalAppointments(appointmentPreferences);
        }


        public ObservableCollection<MedicalAppointment> GetUnavailableMedicalAppointmentsInNextHour(AppointmentPreferences appointmentPreferences)
        {
            //luka
            return MedicalAppointmentService.Instance.GetUnavailableMedicalAppointmentsInNextHour(appointmentPreferences);
        }


        public void ChangeToNew(int id, Interval selectedInterval)
        {
            var medicalAppointment = instance.GetByID(id);
            medicalAppointment.Interval = selectedInterval;
            MedicalAppointmentController.instance.Update(medicalAppointment);
        }

     
    }
}