using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace ZdravoCorpAppTim22.Repository.Generic
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, IHasID
    {
        protected static readonly object _lock = new object();
        public readonly GenericFileHandler<T> FileHandler;
        public List<T> List = new List<T>();

        public event EventHandler DataChangedEvent;
        
        public GenericRepository(string fileName)
        {
            FileHandler = new GenericFileHandler<T>(fileName);
        }
        public void OnDataChangedEvent()
        {
            DataChangedEvent?.Invoke(this, EventArgs.Empty);
        }
        public virtual void Load()
        {
            lock (_lock)
            {
                List = FileHandler.LoadData();
                OnDataChangedEvent();
            }
        }
        public virtual List<T> GetAll()
        {
            return List;
        }
        public virtual T GetByID(int id)
        {
            lock (_lock)
            {
                int index = List.FindIndex(r => r.Id == id);
                if (index == -1)
                {
                    return null;
                }
                return List[index];
            }
        }
        public virtual void DeleteByID(int id)
        {
            lock (_lock)
            {
                int index = List.FindIndex(r => r.Id == id);
                if (index == -1)
                {
                    return;
                }
                List.RemoveAt(index);
                FileHandler.SaveData(List);
                OnDataChangedEvent();
            }
        }
        public virtual void Create(T obj)
        {
            if (obj == null)
            {
                return;
            }
            lock (_lock)
            {
                if (List.Count > 0)
                {
                    obj.Id = List[List.Count - 1].Id + 1;
                }
                else
                {
                    obj.Id = 0;
                }
                List.Add(obj);
                FileHandler.SaveData(List);
                OnDataChangedEvent();
            }
        }
        public virtual void Update(T obj)
        {
            if (obj == null)
            {
                return;
            }
            lock (_lock)
            {
                int index = List.FindIndex(r => r.Id == obj.Id);
                if (index == -1)
                {
                    return;
                }
                List[index] = obj;
                FileHandler.SaveData(List);
                OnDataChangedEvent();
            }
        }
    }
}
