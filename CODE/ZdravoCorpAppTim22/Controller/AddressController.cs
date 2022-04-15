using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
    public class AddressController : GenericController<AddressService, Address>
    {
        private static AddressController instance;
        private AddressController() : base(AddressService.Instance) { }
        public static AddressController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddressController();
                }

                return instance;
            }
        }
    }
}