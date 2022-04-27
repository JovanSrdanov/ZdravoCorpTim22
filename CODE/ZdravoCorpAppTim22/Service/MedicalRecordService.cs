using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalRecordService : GenericService<MedicalRecordRepository, MedicalRecord>
    {
        private static MedicalRecordService instance;
        private MedicalRecordService() : base(MedicalRecordRepository.Instance) { }
        public static MedicalRecordService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordService();
                }
                return instance;
            }
        }
    }
}
