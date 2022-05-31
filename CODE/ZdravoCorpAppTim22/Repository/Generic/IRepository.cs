using System.Collections.Generic;

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
