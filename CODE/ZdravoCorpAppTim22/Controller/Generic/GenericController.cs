using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Controller.Generic
{
    public abstract class GenericController<Service, T> : IController<T> where Service : IService<T>
    {
        private readonly Service _Service;
        public GenericController(Service service)
        {
            _Service = service;
        }
        public void Load()
        {
            _Service.Load();
        }
        public List<T> GetAll()
        {
            return _Service.GetAll();
        }
        public T GetByID(int id)
        {
            return _Service.GetByID(id);

        }
        public void DeleteByID(int id)
        {
            _Service.DeleteByID(id);
        }
        public void Create(T obj)
        {
            _Service.Create(obj);
        }
        public void Update(T obj)
        {
            _Service.Update(obj);
        }
    }
}
