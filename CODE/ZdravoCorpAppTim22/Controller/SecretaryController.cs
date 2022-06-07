using Model;
using Service;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
    public class SecretaryController : GenericController<SecretaryService, SecretaryClass>
    {
        private static SecretaryController instance;
        private SecretaryController() : base(SecretaryService.Instance) { }
        public static SecretaryController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryController();
                }

                return instance;
            }
        }
    }
}