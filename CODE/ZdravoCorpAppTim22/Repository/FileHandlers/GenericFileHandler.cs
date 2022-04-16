using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    public class GenericFileHandler<T>
    {
        private Serializer<List<T>> serializer;

        public GenericFileHandler(string FileName)
        {
            serializer = new Serializer<List<T>>(FileName);
        }

        public void SaveData(List<T> addresses)
        {
            serializer.Serialize(addresses);
        }

        public List<T> LoadData()
        {
            List<T> list = serializer.Deserialize();
            return list == null ? new List<T>() : list;
        }
    }
}
