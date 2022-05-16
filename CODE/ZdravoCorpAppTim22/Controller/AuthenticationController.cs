using Model;
using System;
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

        public event EventHandler LoggedOutEvent;

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
            LoggedOutEvent.Invoke(this, new EventArgs());
        }

        public User GetLoggedUser()
        {
            return AuthenticationService.Instance.LoggedUser;
        }
    }
}
