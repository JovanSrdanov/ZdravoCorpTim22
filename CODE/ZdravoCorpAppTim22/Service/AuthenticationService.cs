using Model;
using Service;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class AuthenticationService
    {
        private static AuthenticationService instance;

        private List<User> Accounts { get; set; }

        private AuthenticationService() 
        {
            Accounts = new List<User>();
        }
        public static AuthenticationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenticationService();
                }

                return instance;
            }
        }

        private List<User> GetAccounts<T, T_Service>(T_Service service) where T_Service : IService<T>
        {
            T_Service _service = service;
            List<User> list = new List<User>();
            foreach (T user in _service.GetAll())
            {

            }
            return list;
        }

        public void Load()
        {
            foreach(ManagerClass user in ManagerService.Instance.GetAll())
            {

            }
        }

    }
}
