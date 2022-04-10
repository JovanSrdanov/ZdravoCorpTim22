// File:    AddressService.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:26:37
// Purpose: Definition of Class AddressService

using Model;
using System.Collections.Generic;

namespace Service
{
    public class AddressService
    {
        public List<Address> GetAll()
        {
            return addressRepository.GetAll();
        }

        public Model.Address GetByID(int id)
        {
            return addressRepository.GetByID(id);

        }

        public void DeleteByID(int id)
        {
            addressRepository.DeleteByID(id);
        }

        public void Create(Model.Address addres)
        {
            addressRepository.Create(addres);
        }

        public void Update(Model.Address addres)
        {
            addressRepository.Update(addres);
        }

        public Repository.AddressRepository addressRepository;

    }
}