// File:    AddressController.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:42:08
// Purpose: Definition of Class AddressController

using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class AddressController
   {
        public List<Address> GetAll()
        {
            return addressService.GetAll();
        }

        public Model.Address GetByID(int id)
        {
            return addressService.GetByID(id);

        }

        public void DeleteByID(int id)
        {
            addressService.DeleteByID(id);
        }

        public void Create(Model.Address addres)
        {
            addressService.Create(addres);
        }

        public void Update(Model.Address addres)
        {
            addressService.Update(addres);
        }

        public String path;
      
      public Service.AddressService addressService;
   
   }
}