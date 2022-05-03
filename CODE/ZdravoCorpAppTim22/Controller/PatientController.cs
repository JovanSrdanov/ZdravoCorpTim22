using Model;
using Service;
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
    }
}