using System.Collections.Generic;
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

        public List<Medicine> GetAllFree()
        {
            List<Medicine> list = new List<Medicine>();
            foreach (Medicine m in GetAll())
            {
                if (m.MedicalReceipt == null)
                {
                    list.Add(m);
                }
            }
            return list;
        }

        public List<Medicine> GetAllApproved()
        {
            List<Medicine> list = new List<Medicine>();
            foreach (Medicine m in GetAllFree())
            {
                if (m.MedicineData.Approval.IsApproved)
                {
                    list.Add(m);
                }
            }
            return list;
        }
    }
}
