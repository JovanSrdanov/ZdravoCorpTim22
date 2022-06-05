using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalReceiptService : GenericService<MedicalReceiptRepository, MedicalReceipt>
    {
        private MedicalReceiptService() : base(MedicalReceiptRepository.Instance) { }
        public static MedicalReceiptService Instance
        {
            get
            {
                return new MedicalReceiptService();
            }
        }
    }
}
