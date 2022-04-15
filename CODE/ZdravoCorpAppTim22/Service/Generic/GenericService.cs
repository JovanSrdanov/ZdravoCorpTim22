using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Load()
        {
            _Repository.Load();
        }
        public List<T> GetAll()
        {
            return _Repository.GetAll();
        }
        public T GetByID(int id)
        {
            return _Repository.GetByID(id);

        }
        public void DeleteByID(int id)
        {
            _Repository.DeleteByID(id);
        }
        public void Create(T obj)
        {
            _Repository.Create(obj);
        }
        public void Update(T obj)
        {
            _Repository.Update(obj);
        }
    }
}
