using Model;
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

        public List<Manager> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model.Manager GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Model.Manager manager)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Manager manager)
        {
            throw new NotImplementedException();
        }

        public String path;

        public Service.ManagerService managerService;

    }
}