using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class AuthenticationService
    {
        private static AuthenticationService instance;
        
        private List<User> Users { get; set; }
        public User LoggedUser { get; private set; }

        private AuthenticationService() 
        {
            Users = new List<User>();
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
        public void Load()
        {
            Users.Clear();
            Users.AddRange(GetAccounts<ManagerClass, ManagerService>(ManagerService.Instance));
            Users.AddRange(GetAccounts<SecretaryClass, SecretaryService>(SecretaryService.Instance));
            Users.AddRange(GetAccounts<Doctor, DoctorService>(DoctorService.Instance));
            Users.AddRange(GetAccounts<Patient, PatientService>(PatientService.Instance));
        }

        public User Login(string email, string password)
        {
            foreach(User user in Users)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                {
                    LoggedUser = user;
                    return user;
                }
            }
            return null;
        }

        public void Logout()
        {
            LoggedUser = null;
            Load();
        }


        private List<User> GetAccounts<T, T_Service>(T_Service service) where T : User where T_Service : IService<T>
        {
            T_Service _service = service;
            List<User> list = new List<User>();
            foreach (T user in _service.GetAll())
            {
                list.Add(user);
            }
            return list;
        }
    }
}
