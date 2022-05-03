using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class MedicalReceiptRepository : GenericRepository<MedicalReceipt>
    {
        public static string FileName = "MedicalReceiptData.json";
        private static MedicalReceiptRepository instance;
        private MedicalReceiptRepository() : base(FileName) { }
        public static MedicalReceiptRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReceiptRepository();
                }
                return instance;
            }
        }
    }
}
