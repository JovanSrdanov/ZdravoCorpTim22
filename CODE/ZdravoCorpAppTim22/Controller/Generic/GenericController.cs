using System.Collections.Generic;
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
        public virtual void Load()
        {
            _Service.Load();
        }
        public virtual List<T> GetAll()
        {
            return _Service.GetAll();
        }
        public virtual T GetByID(int id)
        {
            return _Service.GetByID(id);

        }
        public virtual void DeleteByID(int id)
        {
            _Service.DeleteByID(id);
        }
        public virtual void Create(T obj)
        {
            _Service.Create(obj);
        }
        public virtual void Update(T obj)
        {
            _Service.Update(obj);
        }
    }
}
