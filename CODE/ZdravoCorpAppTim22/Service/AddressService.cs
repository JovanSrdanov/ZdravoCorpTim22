using Repository;
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