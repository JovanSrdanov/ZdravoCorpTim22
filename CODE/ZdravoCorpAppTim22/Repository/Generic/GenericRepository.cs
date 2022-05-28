using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace ZdravoCorpAppTim22.Repository.Generic
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, IHasID
    {
        public readonly object _lock = new object();
        public readonly GenericFileHandler<T> FileHandler;
        public List<T> List = new List<T>();
        public GenericRepository(string fileName)
        {
            FileHandler = new GenericFileHandler<T>(fileName);
        }

        public virtual void Load()
        {
            List = new List<T>(FileHandler.LoadData()); 
        }
        public virtual List<T> GetAll()
        {
            return List;
        }
        public virtual T GetByID(int id)
        {
            return List.Where(r => r.Id == id).FirstOrDefault();
        }
        public virtual void DeleteByID(int id)
        {
            T item = List.Where(r => r.Id == id).FirstOrDefault();
            if(item == null)
            {
                return;
            }
            List.Remove(item);
            FileHandler.SaveData(new List<T>(List));
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
            FileHandler.SaveData(new List<T>(List));
        }
        public virtual void Update(T obj)
        {
            if (obj == null)
            {
                return;
            }
            T item = List.Where(r => r.Id == obj.Id).FirstOrDefault();
            if (item == null)
            {
                return;
            }
            int index = List.IndexOf(item);
            List[index] = obj;
            FileHandler.SaveData(new List<T>(List));
        }
    }
}
