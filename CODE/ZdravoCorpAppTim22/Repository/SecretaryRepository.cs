using Model;
using ZdravoCorpAppTim22.Repository.Generic;
namespace Repository
{
    public class SecretaryRepository : GenericRepository<SecretaryClass>
    {
        public static string FileName = "SecretaryData.json";
        private static SecretaryRepository instance;
        private SecretaryRepository() : base(FileName) { }
        public static SecretaryRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryRepository();
                }

                return instance;
            }
        }
    }
}