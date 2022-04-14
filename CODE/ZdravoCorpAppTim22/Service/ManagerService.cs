using Model;
using Repository;
using System;
using System.Collections.Generic;
namespace Service
{
    public class ManagerService
    {
        private static ManagerService instance;

        private ManagerService()
        {

        }

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

        public List<ManagerClass> GetAll()
        {
            return ManagerRepository.Instance.GetAll();
        }

        public ManagerClass GetByID(int id)
        {
            return ManagerRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            ManagerRepository.Instance.DeleteByID(id);
        }

        public void Create(ManagerClass manager)
        {
            ManagerRepository.Instance.Create(manager);
        }

        public void Update(ManagerClass manager)
        {
            ManagerRepository.Instance.Update(manager);
        }
    }
}