using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
