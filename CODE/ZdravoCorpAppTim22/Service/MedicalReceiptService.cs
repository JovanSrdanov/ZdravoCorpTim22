using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalReceiptService : GenericService<MedicalReceiptRepository, MedicalReceipt>
    {
        private static MedicalReceiptService instance;
        private MedicalReceiptService() : base(MedicalReceiptRepository.Instance) { }
        public static MedicalReceiptService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReceiptService();
                }
                return instance;
            }
        }
    }
}
