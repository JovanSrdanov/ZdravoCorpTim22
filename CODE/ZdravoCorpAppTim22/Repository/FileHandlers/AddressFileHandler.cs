using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    public class AddressFileHandler
    {
        private Serializer<List<Address>> serializer;

        public AddressFileHandler(string FileName)
        {
            serializer = new Serializer<List<Address>>(FileName);
        }

        public void SaveData(List<Address> addresses)
        {
            serializer.Serialize(addresses);
        }

        public List<Address> LoadData()
        {
            List<Address> addresses = serializer.Deserialize();
            return addresses == null ? new List<Address>() : addresses;
        }
    }
}
