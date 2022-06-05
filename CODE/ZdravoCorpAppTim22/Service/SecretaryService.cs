using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class SecretaryService : GenericService<SecretaryRepository, SecretaryClass>
    {
        private SecretaryService() : base(SecretaryRepository.Instance) { }
        public static SecretaryService Instance
        {
            get
            {
                return new SecretaryService();
            }
        }
    }
}