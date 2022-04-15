using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class SecretaryController
    {


        private static SecretaryController instance;

        private SecretaryController()
        {

        }

        public static SecretaryController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryController();
                }

                return instance;
            }
        }
        public void Load()
        {
            SecretaryService.Instance.Load();
        }

        public List<SecretaryClass> GetAll()
        {
            return SecretaryService.Instance.GetAll();
        }

        public Model.SecretaryClass GetByID(int id)
        {
            return SecretaryService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            SecretaryService.Instance.DeleteByID(id);
        }

        public void Create(SecretaryClass secretary)
        {
            SecretaryService.Instance.Create(secretary);
        }

        public void Update(SecretaryClass secretary)
        {
            SecretaryService.Instance.Update(secretary);
        }
    }
}