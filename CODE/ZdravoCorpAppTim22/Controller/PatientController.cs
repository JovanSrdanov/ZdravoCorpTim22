using Model;
using Service;
using System;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

namespace Controller
{
    public class PatientController : GenericController<PatientService, Patient>
    {
        private static PatientController instance;
        private PatientController() : base(PatientService.Instance) { }
        public static PatientController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientController();
                }

                return instance;
            }
        }

        public Patient GetPatient(Patient patient)
        {
            return PatientService.Instance.GetPatient(patient);
        }
        
        public string TherapyNotification()
        {
            return PatientService.Instance.TherapyNotification();
        }

        public bool AntiTroll(Patient patient)
        {

           return PatientService.Instance.AntiTroll(patient);

        }
        public void checkIfPatientHasMedicalRecord(Patient patient)
        {
            PatientService.Instance.checkIfPatientHasMedicalRecord(patient);
        }

        public string PersonalNoteNotification()
        {
            return PatientService.Instance.PersonalNoteNotification();
        }
    }
}