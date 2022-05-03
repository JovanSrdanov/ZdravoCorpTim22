using Model;
using Repository;
using System.Windows;
using ZdravoCorpAppTim22.Service.Generic;
using ZdravoCorpAppTim22.View.PatientView;

namespace Service
{
    public class PatientService : GenericService<PatientRepository, Patient>
    {
        private static PatientService instance;
        private PatientService() : base(PatientRepository.Instance) { }
        public static PatientService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }

                return instance;
            }
        }

        public void DaemonMethod()
        {
            if (PatientSelectionForTemporaryPurpose.LoggedPatient == null)
            {
           
                return;
            }
            else
            {
                MessageBox.Show("Ulogovan");
            }
        }
    }
}