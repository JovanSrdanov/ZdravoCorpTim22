using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class ManagerService : GenericService<ManagerRepository, ManagerClass>
    {
        private ManagerService() : base(ManagerRepository.Instance) { }
        public static ManagerService Instance
        {
            get
            {
                return new ManagerService();
            }
        }
    }
}