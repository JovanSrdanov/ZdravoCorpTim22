using Model;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class PatientRepository : GenericRepository<Patient>
    {
        public static string FileName = "PatientData.json";
        private static PatientRepository instance;
        private PatientRepository() : base(FileName) { }
        public static PatientRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientRepository();
                }

                return instance;
            }
        }

        public Patient GetPatient(Patient patient)
        {
            ObservableCollection<Patient> observableCollection = new ObservableCollection<Patient>(GetAll());
            for (int i = 0; i < observableCollection.Count; i++)
            {
                if (observableCollection[i] == patient)
                {
                    return observableCollection[i];
                }
            }

            return null;
        }
    }
}