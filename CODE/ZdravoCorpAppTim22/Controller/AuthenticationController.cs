using Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class AuthenticationController
    {
        private static AuthenticationController instance;
        private AuthenticationController() { }
        public static AuthenticationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenticationController();
                }

                return instance;
            }
        }

        public void Load()
        {
            AuthenticationService.Instance.Load();
        }

        public User Login(string email, string password)
        {
            return AuthenticationService.Instance.Login(email, password);
        }
        public void Logout()
        {
            AuthenticationService.Instance.Logout();
        }

        public User GetLoggedUser()
        {
            return AuthenticationService.Instance.LoggedUser;
        }
    }
}
