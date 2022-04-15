using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class RenovationController
    {
        private static RenovationController instance;
        private RenovationController() { }
        public static RenovationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationController();
                }
                return instance;
            }
        }
        public void Load()
        {
            RenovationService.Instance.Load();
        }
        public List<Renovation> GetAll()
        {
            return RenovationService.Instance.GetAll();
        }

        public Renovation GetByID(int id)
        {
            return RenovationService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            RenovationService.Instance.DeleteByID(id);
        }

        public void Create(Renovation renovation)
        {
            RenovationService.Instance.Create(renovation);
        }

        public void Update(Renovation renovation)
        {
            RenovationService.Instance.Update(renovation);
        }

    }
}