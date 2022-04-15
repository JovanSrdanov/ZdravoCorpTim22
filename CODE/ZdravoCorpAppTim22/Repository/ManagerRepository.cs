using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class ManagerRepository
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
            int index = managers.FindIndex(r => r.ID == id);
            return managers[index];
        }

        public void DeleteByID(int id)
        {
            int index = managers.FindIndex(r => r.ID == id);
            managers.RemoveAt(index);
            managerFileHandler.SaveData(managers);
        }

        public void Create(ManagerClass manager)
        {
            if (managers.Count > 0)
            {
                manager.ID = managers[managers.Count - 1].ID + 1;
            }
            else
            {
                manager.ID = 0;
            }

            managers.Add(manager);
            managerFileHandler.SaveData(managers);
        }

        public void Update(ManagerClass manager)
        {
            int index = managers.FindIndex(r => r.ID == manager.ID);
            managers[index] = manager;
            managerFileHandler.SaveData(managers);
        }

        public String path;

    }
}