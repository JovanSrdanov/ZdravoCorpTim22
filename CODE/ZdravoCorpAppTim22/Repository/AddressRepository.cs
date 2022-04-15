// File:    AddressRepository.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:20:04
// Purpose: Definition of Class AddressRepository

using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class AddressRepository
    {
        private static AddressRepository instance;
        public string FileName = "AddressData.json";

        List<Address> addresses = new List<Address>();
        GenericFileHandler<Address> AddressFileHandler;
        private AddressRepository()
        {
            AddressFileHandler = new GenericFileHandler<Address>(FileName);
        }

        public static AddressRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddressRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            addresses = AddressFileHandler.LoadData();
        }

        public List<Address> GetAll()
        {
            return this.addresses;
        }

        public Address GetByID(int id)
        {
            int index = addresses.FindIndex(r => r.ID == id);
            if (index == -1) return null;
            return addresses[index];
        }

        public void DeleteByID(int id)
        {
            int index = addresses.FindIndex(r => r.ID == id);
            addresses.RemoveAt(index);
            AddressFileHandler.SaveData(addresses);
        }

        public void Create(Address address)
        {
            if (addresses.Count > 0)
            {
                address.ID = addresses[addresses.Count - 1].ID + 1;
            }
            else
            {
                address.ID = 0;
            }

            addresses.Add(address);
            AddressFileHandler.SaveData(addresses);
        }

        public void Update(Address address)
        {
            int index = addresses.FindIndex(r => r.ID == address.ID);
            addresses[index] = address;
            AddressFileHandler.SaveData(addresses);
        }

    }
}