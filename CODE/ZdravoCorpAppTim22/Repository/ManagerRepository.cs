using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class ManagerRepository : GenericRepository<ManagerClass>
    {
        public static string FileName = "ManagerData.json";
        private static ManagerRepository instance;
        private ManagerRepository() : base(FileName) { }
        public static ManagerRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerRepository();
                }

                return instance;
            }
        }
    }
}