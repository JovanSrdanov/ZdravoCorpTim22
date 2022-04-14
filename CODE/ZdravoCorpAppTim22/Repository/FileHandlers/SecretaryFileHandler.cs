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
            serializer.Serialize(secretaries);
        }

        public List<SecretaryClass> LoadData()
        {
            List<SecretaryClass> secretaries = serializer.Deserialize();
            return secretaries == null ? new List<SecretaryClass>() : secretaries;
        }
    }
}

