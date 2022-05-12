using System.Linq;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicineDataService : GenericService<MedicineDataRepository, MedicineData>
    {
        private static MedicineDataService instance;
        private MedicineDataService() : base(MedicineDataRepository.Instance) { }
        public static MedicineDataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineDataService();
                }
                return instance;
            }
        }

        public MedicineData GetByName(string name)
        {
            foreach(MedicineData item in GetAll())
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }

        public override void DeleteByID(int id)
        {
            foreach(MedicineData medicineData in GetAll())
            {
                MedicineData temp = medicineData.Replacements.Where(x => x.Id == id).FirstOrDefault();
                if(temp != null)
                {
                    medicineData.RemoveReplacement(temp);
                }
            }
            ReplacementService.Instance.DeleteByMedicineData(MedicineDataRepository.Instance.GetByID(id));
            base.DeleteByID(id);
        }
    }
}
