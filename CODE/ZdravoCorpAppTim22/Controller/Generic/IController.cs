using System;
using System.Collections.Generic;

namespace ZdravoCorpAppTim22.Controller.Generic
{
    public interface IController<T>
    {
        event EventHandler DataChangedEvent;
        void Load();
        List<T> GetAll();
        T GetByID(int id);
        void DeleteByID(int id);
        void Create(T obj);
        void Update(T obj);
    }
}
