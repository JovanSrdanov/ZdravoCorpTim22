using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class SecretaryRepository
    {
        private static SecretaryRepository instance;

        private SecretaryRepository()
        {

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
        List<SecretaryClass> secretaries = new List<SecretaryClass>
        {
            new SecretaryClass("Jovan", "Zelic", "jovan@gmail.com", "1231231239", "stefan1239", DateTime.Now, "123321123", Gender.male,  null),
            new SecretaryClass("Erzebet", "Jovanovic", "erzebet@gmail.com", "223123123", "stefan1247", DateTime.Now, "223321123", Gender.female,  null),
            new SecretaryClass("Sulejman", "Kovacevic", "sulejman@gmail.com", "323123123", "stefan12553", DateTime.Now, "323321123", Gender.male,  null),
        };

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
        }

        public void Update(SecretaryClass secretary)
        {
            int index = secretaries.FindIndex(r => r.ID == secretary.ID);
            secretaries[index] = secretary;
        }

        public String path;

    }
}