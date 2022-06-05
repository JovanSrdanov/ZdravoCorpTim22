using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class EquipmentDataService : GenericService<EquipmentDataRepository, EquipmentData>
    {
        private EquipmentDataService() : base(EquipmentDataRepository.Instance) { }
        public static EquipmentDataService Instance
        {
            get
            {
                return new EquipmentDataService();
            }
        }

        public EquipmentData GetByName(string name)
        {
            foreach (EquipmentData item in GetAll())
            {
                if(item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
    }
}