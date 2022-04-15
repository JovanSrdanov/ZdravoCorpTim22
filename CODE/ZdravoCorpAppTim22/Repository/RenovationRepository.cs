using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace ZdravoCorpAppTim22.Repository
{
    public class RenovationRepository
    {
        public string FileName = "RenovationData.json";
        GenericFileHandler<Renovation> RenovationFileHandler;

        List<Renovation> RenovationList = new List<Renovation>();
        private static RenovationRepository instance;
        private RenovationRepository() 
        {
            RenovationFileHandler = new GenericFileHandler<Renovation>(FileName);
        }
        public static RenovationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            RenovationList = RenovationFileHandler.LoadData();
        }
        public List<Renovation> GetAll()
        {
            return this.RenovationList;
        }

        public Renovation GetByID(int id)
        {
            int index = RenovationList.FindIndex(r => r.Id == id);
            return RenovationList[index];
        }

        public void DeleteByID(int id)
        {
            int index = RenovationList.FindIndex(r => r.Id == id);
            RenovationList.RemoveAt(index);
            RenovationFileHandler.SaveData(RenovationList);
        }

        public void Create(Renovation renovation)
        {
            if (RenovationList.Count > 0)
            {
                renovation.Id = RenovationList[RenovationList.Count - 1].Id + 1;
            }
            else
            {
                renovation.Id = 0;
            }
            this.RenovationList.Add(renovation);
            RenovationFileHandler.SaveData(RenovationList);
        }

        public void Update(Renovation renovation)
        {
            int index = RenovationList.FindIndex(r => r.Id == renovation.Id);
            RenovationList[index] = renovation;
            RenovationFileHandler.SaveData(RenovationList);
        }
    }
}
