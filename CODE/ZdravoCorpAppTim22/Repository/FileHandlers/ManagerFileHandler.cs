using Controller;
using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    class ManagerFileHandler
    {
        private Serializer<List<ManagerClass>> serializer;
        public ManagerFileHandler(string Filename)
        {
            serializer = new Serializer<List<ManagerClass>>(Filename);
        }
        public void SaveData(List<ManagerClass> managers)
        {
            serializer.Serialize(managers);
        }

        public List<ManagerClass> LoadData()
        {
            List<ManagerClass> managers = serializer.Deserialize();
            return managers == null ? new List<ManagerClass>() : managers;
        }
    }
}
