using System.Collections.Generic;

namespace ZdravoCorpAppTim22.Service.Generic
{
    public interface IService<T>
    {
        void Load();
        List<T> GetAll();
        T GetByID(int id);
        void DeleteByID(int id);
        void Create(T obj);
        void Update(T obj);
    }
}
