using Controller;
using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    class SecretaryFileHandler
    {
        private Serializer<List<SecretaryClass>> serializer;
        public SecretaryFileHandler(string Filename)
        {
            serializer = new Serializer<List<SecretaryClass>>(Filename);
        }
        public void SaveData(List<SecretaryClass> secretaries)
        {
            foreach (SecretaryClass secretary in secretaries)
            {
                if (secretary.Address != null)
                {
                    secretary.addressID = secretary.address.ID;
                }
                else
                {
                    secretary.addressID = -1;
                }
            }
            serializer.Serialize(secretaries);
        }

        public List<SecretaryClass> LoadData()
        {
            List<SecretaryClass> secretaries = serializer.Deserialize();
            if (secretaries == null)
            {
                return new List<SecretaryClass>();
            }
            foreach (SecretaryClass secretary in secretaries)
            {
                if (secretary.addressID != -1)
                {
                    Address ad = AddressController.Instance.GetByID(secretary.addressID);
                    if (ad != null)
                    {
                        secretary.Address = ad;
                    }
                    else
                    {
                        secretary.addressID = -1;
                    }
                }
            }
            return secretaries == null ? new List<SecretaryClass>() : secretaries;
        }
    }
}

