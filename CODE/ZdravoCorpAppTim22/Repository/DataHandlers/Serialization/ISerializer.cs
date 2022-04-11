using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Repository.DataHandlers.Serialization
{
    internal interface ISerializer<T> where T : class
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
