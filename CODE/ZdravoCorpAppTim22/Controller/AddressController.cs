using Service;
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

        public void Load()
        {
            AddressService.Instance.Load();
        }

        public List<Address> GetAll()
        {
            return AddressService.Instance.GetAll();
        }

        public Address GetByID(int id)
        {
            return AddressService.Instance.GetByID(id);

        }

        public void DeleteByID(int id)
        {
            AddressService.Instance.DeleteByID(id);
        }

        public void Create(Address addres)
        {
            AddressService.Instance.Create(addres);
        }

        public void Update(Address addres)
        {
            AddressService.Instance.Update(addres);
        }
    }
}