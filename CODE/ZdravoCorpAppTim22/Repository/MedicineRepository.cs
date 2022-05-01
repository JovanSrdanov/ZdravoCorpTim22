using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class MedicineRepository : GenericRepository<Medicine>
    {
        public static string FileName = "MedicineData.Json";
        private static MedicineRepository instance;
        private MedicineRepository() : base(FileName) { }
        public static MedicineRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineRepository();
                }
                return instance;
            }
        }
    }
}
