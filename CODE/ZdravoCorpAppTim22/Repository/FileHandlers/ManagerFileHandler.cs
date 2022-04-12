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
            foreach (ManagerClass manager in managers)
            {
                if (manager.Address != null)
                {
                    manager.addressID = manager.address.ID;
                }
                else
                {
                    manager.addressID = -1;
                }
            }
            serializer.Serialize(managers);
        }

        public List<ManagerClass> LoadData()
        {
            List<ManagerClass> managers = serializer.Deserialize();
            if (managers == null)
            {
                return new List<ManagerClass>();
            }
            foreach (ManagerClass manager in managers)
            {
                if (manager.addressID != -1)
                {
                    Address ad = AddressController.Instance.GetByID(manager.addressID);
                    if (ad != null)
                    {
                        manager.Address = ad;
                    }
                    else
                    {
                        manager.addressID = -1;
                    }
                }
            }
            return managers == null ? new List<ManagerClass>() : managers;
        }
    }
}
