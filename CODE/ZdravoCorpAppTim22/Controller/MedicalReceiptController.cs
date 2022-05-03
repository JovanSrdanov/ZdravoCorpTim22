using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class MedicalReceiptController : GenericController<MedicalReceiptService, MedicalReceipt>
    {
        private static MedicalReceiptController instance;
        private MedicalReceiptController() : base(MedicalReceiptService.Instance) { }
        public static MedicalReceiptController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReceiptController();
                }
                return instance;
            }
        }
    }
}
