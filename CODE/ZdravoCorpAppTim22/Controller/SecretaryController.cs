using Model;
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

        public List<Secretary> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model.Secretary GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Model.Secretary secretary)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Secretary secretary)
        {
            throw new NotImplementedException();
        }

        public String path;

        public Service.SecretaryService secretaryService;

    }
}