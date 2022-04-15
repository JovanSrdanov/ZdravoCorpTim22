using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;

namespace ZdravoCorpAppTim22.Service
{
    public class RenovationService
    {
        private static RenovationService instance;

        private RenovationService()
        {

        }

        public static RenovationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationService();
                }

                return instance;
            }
        }

        public void Load()
        {
            RenovationRepository.Instance.Load();
        }
        public List<Renovation> GetAll()
        {
            return RenovationRepository.Instance.GetAll();
        }

        public Renovation GetByID(int id)
        {
            return RenovationRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            RenovationRepository.Instance.DeleteByID(id);
        }

        public void Create(Renovation renovation)
        {
            RenovationRepository.Instance.Create(renovation);
        }

        public void Update(Renovation renovation)
        {
            RenovationRepository.Instance.Update(renovation);
        }

    }
}