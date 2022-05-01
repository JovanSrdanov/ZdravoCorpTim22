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
    public class MedicineController : GenericController<MedicineService, Medicine>
    {
        private static MedicineController instance;
        private MedicineController() : base(MedicineService.Instance) { }
        public static MedicineController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineController();
                }
                return instance;
            }
        }
    }
}
