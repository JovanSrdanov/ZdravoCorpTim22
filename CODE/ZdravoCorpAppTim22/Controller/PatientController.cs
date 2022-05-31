using Model;
using Service;
using System;
using ZdravoCorpAppTim22.Controller.Generic;

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
        
        public void TherapyNotification()
        {
            PatientService.Instance.TherapyNotification();
        }

        public void AntiTroll(Patient patient)
        {

            PatientService.Instance.AntiTroll(patient);

        }

        ///STEFAN DODAO
        public void checkIfPatientHasMedicalRecord(Patient patient)
        {
            PatientService.Instance.checkIfPatientHasMedicalRecord(patient);
        }   
        ///STEFAN DODAO
    }
}