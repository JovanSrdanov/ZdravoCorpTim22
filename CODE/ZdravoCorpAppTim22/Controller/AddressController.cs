using System;
using System.Collections.Generic;

namespace Controller
{
    public class AddressController
    {

        private static AddressController instance;

        private AddressController()
        {

        }
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

        public List<Address> GetAll()
        {
            return addressService.GetAll();
        }

        public Address GetByID(int id)
        {
            return addressService.GetByID(id);

        }

        public void DeleteByID(int id)
        {
            addressService.DeleteByID(id);
        }

        public void Create(Address addres)
        {
            addressService.Create(addres);
        }

        public void Update(Address addres)
        {
            addressService.Update(addres);
        }

        public String path;

        public Service.AddressService addressService;

    }
}