using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class ManagerController
    {

        private static ManagerController instance;

        private ManagerController()
        {

        }
        public static ManagerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerController();
                }

                return instance;
            }
        }

        public List<ManagerClass> GetAll()
        {
            return ManagerService.Instance.GetAll();
        }

        public Model.ManagerClass GetByID(int id)
        {
            return ManagerService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            ManagerService.Instance.DeleteByID(id);
        }

        public void Create(ManagerClass manager)
        {
            ManagerService.Instance.Create(manager);
        }

        public void Update(ManagerClass manager)
        {
            ManagerService.Instance.Update(manager);
        }

        public String path;

    }
}