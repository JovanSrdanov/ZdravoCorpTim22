using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class ManagerService : GenericService<ManagerRepository, ManagerClass>
    {
        private static ManagerService instance;
        private ManagerService() : base(ManagerRepository.Instance) { }
        public static ManagerService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerService();
                }

                return instance;
            }
        }
    }
}