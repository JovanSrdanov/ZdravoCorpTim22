using System.Collections.Generic;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace ZdravoCorpAppTim22.Repository.Generic
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, IHasID
    {
        public readonly GenericFileHandler<T> FileHandler;
        public List<T> List = new List<T>();
        public GenericRepository(string fileName)
        {
            FileHandler = new GenericFileHandler<T>(fileName);
        }
        public virtual void Load()
        {
            List = FileHandler.LoadData();
        }
        public virtual List<T> GetAll()
        {
            return List;
        }
        public virtual T GetByID(int id)
        {
            int index = List.FindIndex(r => r.Id == id);
            if (index == -1)
            {
                return null;
            }
            return List[index];
        }
        public virtual void DeleteByID(int id)
        {
            int index = List.FindIndex(r => r.Id == id);
            if (index == -1)
            {
                return;
            }
            List.RemoveAt(index);
            FileHandler.SaveData(List);
        }
        public virtual void Create(T obj)
        {
            if (obj == null)
            {
                return;
            }
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
        }
        public virtual void Update(T obj)
        {
            if (obj == null)
            {
                return;
            }
            int index = List.FindIndex(r => r.Id == obj.Id);
            if (index == -1)
            {
                return;
            }
            List[index] = obj;
            FileHandler.SaveData(List);
        }
    }
}
