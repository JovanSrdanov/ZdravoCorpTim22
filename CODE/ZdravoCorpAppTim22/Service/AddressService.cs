// File:    AddressService.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:26:37
// Purpose: Definition of Class AddressService

using Repository;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class AddressService : GenericService<AddressRepository, Address>
    {
        private static AddressService instance;
        private AddressService() : base(AddressRepository.Instance) { }
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
    }
}