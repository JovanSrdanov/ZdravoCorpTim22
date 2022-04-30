using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class AddressRepository : GenericRepository<Address>
    {
        public static string FileName = "AddressData.json";
        private static AddressRepository instance;
        private AddressRepository() : base(FileName) {}
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
    }
}