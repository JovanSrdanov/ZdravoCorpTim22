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
    public class MedicineService : GenericService<MedicineRepository, Medicine>
    {
        private static MedicineService instance;
        private MedicineService() : base(MedicineRepository.Instance) { }
        public static MedicineService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineService();
                }
                return instance;
            }
        }
    }
}
