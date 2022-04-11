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
        List<Secretary> secretaries = new List<Secretary>
        {
            new Secretary("Jovan", "Zelic", "jovan@gmail.com", "1231231239", "stefan1239", DateTime.Now, "123321123", Gender.male, 130, null),
            new Secretary("Erzebet", "Jovanovic", "erzebet@gmail.com", "223123123", "stefan1247", DateTime.Now, "223321123", Gender.female, 131, null),
            new Secretary("Sulejman", "Kovacevic", "sulejman@gmail.com", "323123123", "stefan12553", DateTime.Now, "323321123", Gender.male, 132, null),
        };

        public List<Secretary> GetAll()
        {
            return secretaries;
        }

        public Secretary GetByID(int id)
        {
            int index = secretaries.FindIndex(r => r.ID == id);
            return secretaries[index];
        }

        public void DeleteByID(int id)
        {
            int index = secretaries.FindIndex(r => r.ID == id);
            secretaries.RemoveAt(index);
        }

        public void Create(Secretary secretary)
        {
            this.secretaries.Add(secretary);
        }

        public void Update(Secretary secretary)
        {
            int index = secretaries.FindIndex(r => r.ID == secretary.ID);
            secretaries[index] = secretary;
        }

        public String path;

    }
}