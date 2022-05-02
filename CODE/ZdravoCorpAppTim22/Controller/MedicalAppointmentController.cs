using Model;
using Service;
using System;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

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
            return MedicalAppointmentService.Instance.GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime, enteredAppointmentType, enteredPriority, enteredDoctor);

        }


    }
}