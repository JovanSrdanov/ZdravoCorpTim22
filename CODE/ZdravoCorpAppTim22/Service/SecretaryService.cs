using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class SecretaryService : GenericService<SecretaryRepository, SecretaryClass>
    {
        private static SecretaryService instance;
        private SecretaryService() : base(SecretaryRepository.Instance) { }
        public static SecretaryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryService();
                }

                return instance;
            }
        }
    }
}