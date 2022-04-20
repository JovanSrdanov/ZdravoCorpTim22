using Repository;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
   public class EquipmentRelocationService : GenericService<EquipmentRelocationRepository, EquipmentRelocation>
    {
        private static EquipmentRelocationService instance;
        private EquipmentRelocationService() : base(EquipmentRelocationRepository.Instance) { }
        public static EquipmentRelocationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRelocationService();
                }
                return instance;
            }
        }
   }
}