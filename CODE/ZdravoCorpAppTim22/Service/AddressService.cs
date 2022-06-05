using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class AddressService : GenericService<AddressRepository, Address>
    {
        private AddressService() : base(AddressRepository.Instance) { }
        public static AddressService Instance
        {
            get
            {
                return new AddressService();
            }
        }
    }
}