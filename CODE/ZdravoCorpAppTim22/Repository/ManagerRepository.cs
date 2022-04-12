using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class ManagerRepository
    {
        private static ManagerRepository instance;

        private ManagerRepository()
        {

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

        List<ManagerClass> managers = new List<ManagerClass>
        {
            new ManagerClass("Boban", "Antonic", "Boban@gmail.com", "1231231231", "stefan123", DateTime.Now, "123321123", Gender.male, 126,  null),
            new ManagerClass("Slavko", "Malinovic", "slavko@gmail.com", "2231231232", "stefan124", DateTime.Now, "223321123", Gender.male, 127,  null),
            new ManagerClass("Vinka", "Lazic", "vinka@gmail.com", "3231231233", "stefan125", DateTime.Now, "323321123", Gender.female, 128,  null),
        };

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
        }

        public void Create(ManagerClass manager)
        {
            this.managers.Add(manager);
        }

        public void Update(ManagerClass manager)
        {
            int index = managers.FindIndex(r => r.ID == manager.ID);
            managers[index] = manager;
        }

        public String path;

    }
}