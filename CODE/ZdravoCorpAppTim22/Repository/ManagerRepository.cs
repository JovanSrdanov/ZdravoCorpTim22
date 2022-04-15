using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class ManagerRepository : IRepository<ManagerClass>
    {
        private static ManagerRepository instance;

        public string FileName = "ManagerData.json";
        GenericFileHandler<ManagerClass> managerFileHandler;
        List<ManagerClass> managers = new List<ManagerClass>();

        private ManagerRepository()
        {
            managerFileHandler = new GenericFileHandler<ManagerClass>(FileName);
        }

        public static ManagerRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            managers = managerFileHandler.LoadData();
        }

        public List<ManagerClass> GetAll()
        {
            return managers;
        }

        public ManagerClass GetByID(int id)
        {
            int index = managers.FindIndex(r => r.Id == id);
            return managers[index];
        }

        public void DeleteByID(int id)
        {
            int index = managers.FindIndex(r => r.Id == id);
            managers.RemoveAt(index);
            managerFileHandler.SaveData(managers);
        }

        public void Create(ManagerClass manager)
        {
            if (managers.Count > 0)
            {
                manager.Id = managers[managers.Count - 1].Id + 1;
            }
            else
            {
                manager.Id = 0;
            }

            managers.Add(manager);
            managerFileHandler.SaveData(managers);
        }

        public void Update(ManagerClass manager)
        {
            int index = managers.FindIndex(r => r.Id == manager.Id);
            managers[index] = manager;
            managerFileHandler.SaveData(managers);
        }

        public String path;

    }
}