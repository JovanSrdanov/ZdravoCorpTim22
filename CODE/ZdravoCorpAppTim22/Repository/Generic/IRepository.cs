using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Repository.Generic
{
    public interface IRepository<T>
    {
        void Load();
        List<T> GetAll();
        T GetByID(int id);
        void DeleteByID(int id);
        void Create(T obj);
        void Update(T obj);
    }
}
