using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.Repository.Generic
{
    public interface IRepository<T>
    {
        void Load();
        ObservableCollection<T> GetAll();
        T GetByID(int id);
        void DeleteByID(int id);
        void Create(T obj);
        void Update(T obj);
    }
}
