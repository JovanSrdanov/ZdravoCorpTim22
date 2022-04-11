// File:    AddressRepository.cs
// Author:  Jana Jovan
// Created: 10 April, 2022 22:20:04
// Purpose: Definition of Class AddressRepository

using System;
using System.Collections.Generic;

namespace Repository
{
    public class AddressRepository
    {
        private static AddressRepository instance;


        private AddressRepository()
        {

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

        List<Address> adresses = new List<Address>
        {
            new Address("Ulica1","broj1","Grad1","Drzava1",1),
            new Address("Ulica2","broj2","Grad2","Drzava2",2),
            new Address("Ulica3","broj3","Grad3","Drzava3",3),
            new Address("Ulica4","broj4","Grad4","Drzava4",4),
        };


        public List<Address> GetAll()
        {
            return this.adresses;
        }

        public Address GetByID(int id)
        {
            int index = adresses.FindIndex(r => r.ID == id);
            return adresses[index];
        }

        public void DeleteByID(int id)
        {
            int index = adresses.FindIndex(r => r.ID == id);
            adresses.RemoveAt(index);
        }

        public void Create(Address address)
        {
            adresses.Add(address);
        }

        public void Update(Address address)
        {
            int index = adresses.FindIndex(r => r.ID == address.ID);
            adresses[index] = address;
        }

        public String path;

    }
}