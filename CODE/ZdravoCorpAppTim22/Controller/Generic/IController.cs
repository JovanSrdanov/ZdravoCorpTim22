using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Controller.Generic
{
    public interface IController<T>
    {
        void Load();
        List<T> GetAll();
        T GetByID(int id);
        void DeleteByID(int id);
        void Create(T obj);
        void Update(T obj);
    }
}
