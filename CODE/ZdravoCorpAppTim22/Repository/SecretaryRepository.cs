using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;
namespace Repository
{
    public class SecretaryRepository
    {
        private static SecretaryRepository instance;
        public string FileName = "SecretaryData.json";
        GenericFileHandler<SecretaryClass> secretaryFileHandler;
        List<SecretaryClass> secretaries = new List<SecretaryClass>();
        private SecretaryRepository()
        {
            secretaryFileHandler = new GenericFileHandler<SecretaryClass>(FileName);

        }
        public static SecretaryRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            secretaries = secretaryFileHandler.LoadData();
        }

        public List<SecretaryClass> GetAll()
        {
            return secretaries;
        }

        public SecretaryClass GetByID(int id)
        {
            int index = secretaries.FindIndex(r => r.ID == id);
            return secretaries[index];
        }

        public void DeleteByID(int id)
        {
            int index = secretaries.FindIndex(r => r.ID == id);
            secretaries.RemoveAt(index);
            secretaryFileHandler.SaveData(secretaries);
        }

        public void Create(SecretaryClass secretary)
        {
            if (secretaries.Count > 0)
            {
                secretary.ID = secretaries[secretaries.Count - 1].ID + 1;
            }
            else
            {
                secretary.ID = 0;
            }

            secretaries.Add(secretary);
            secretaryFileHandler.SaveData(secretaries);
        }

        public void Update(SecretaryClass secretary)
        {
            int index = secretaries.FindIndex(r => r.ID == secretary.ID);
            secretaries[index] = secretary;
            secretaryFileHandler.SaveData(secretaries);
        }

        public String path;

    }
}