using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class EquipmentService : GenericService<EquipmentRepository, Equipment>
    {
        private static EquipmentService instance;
        private EquipmentService() : base(EquipmentRepository.Instance) { }
        public static EquipmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentService();
                }

                return instance;
            }
        }
    }
}