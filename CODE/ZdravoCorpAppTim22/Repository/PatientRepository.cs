using Model;
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
    }
}