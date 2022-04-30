using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class MedicalAppointmentRepository : GenericRepository<MedicalAppointment>
    {
        public static string FileName = "MedicalAppointmentData.json";
        private static MedicalAppointmentRepository instance;
        private MedicalAppointmentRepository() : base(FileName) { }
        public static MedicalAppointmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentRepository();
                }

                return instance;
            }
        }
    }
}