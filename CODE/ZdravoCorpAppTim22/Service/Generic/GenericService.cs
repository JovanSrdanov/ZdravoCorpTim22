using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Service.Generic
{
    public abstract class GenericService<Repository, T> : IService<T> where Repository : IRepository<T>
    {
        private readonly Repository _Repository;
        public GenericService(Repository repository)
        {
            _Repository = repository;
        }
        public virtual void Load()
        {
            _Repository.Load();
        }
        public virtual ObservableCollection<T> GetAll()
        {
            return _Repository.GetAll();
        }
        public virtual T GetByID(int id)
        {
            return _Repository.GetByID(id);

        }
        public virtual void DeleteByID(int id)
        {
            _Repository.DeleteByID(id);
        }
        public virtual void Create(T obj)
        {
            _Repository.Create(obj);
        }
        public virtual void Update(T obj)
        {
            _Repository.Update(obj);
        }
    }
}
