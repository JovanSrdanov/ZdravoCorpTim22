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
            foreach(MedicineData item in MedicineDataRepository.Instance.GetAll())
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
