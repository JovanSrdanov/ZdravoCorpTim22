// File:    AddressService.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:26:37
// Purpose: Definition of Class AddressService

using Repository;
using System.Collections.Generic;

namespace Service
{
    public class AddressService
    {
        private static AddressService instance;

        private AddressService()
        {
            
        }

        public static AddressService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddressService();
                }

                return instance;
            }
        }

        public List<Address> GetAll()
        {
            return AddressRepository.Instance.GetAll();
        }

        public Address GetByID(int id)
        {
            return AddressRepository.Instance.GetByID(id);

        }

        public void DeleteByID(int id)
        {
            AddressRepository.Instance.DeleteByID(id);
        }

        public void Create(Address addres)
        {
            AddressRepository.Instance.Create(addres);
        }

        public void Update(Address addres)
        {
            AddressRepository.Instance.Update(addres);
        }


    }
}