using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SecretaryService
    {
        private static SecretaryService instance;

        private SecretaryService()
        {

        }

        public static SecretaryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryService();
                }

                return instance;
            }
        }

        public List<Secretary> GetAll()
        {
            return SecretaryRepository.Instance.GetAll();
        }

        public Secretary GetByID(int id)
        {
            return SecretaryRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            SecretaryRepository.Instance.DeleteByID(id);
        }

        public void Create(Model.Secretary secretary)
        {
            SecretaryRepository.Instance.Create(secretary);
        }

        public void Update(Model.Secretary secretary)
        {
            SecretaryRepository.Instance.Update(secretary);
        }

        public String path;

    }
}