using Model;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class RoomRepository : GenericRepository<Room>
    {
        public static string FileName = "RoomData.json";
        private static RoomRepository instance;
        private RoomRepository() : base(FileName) { }
        public static RoomRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomRepository();

                }

                return instance;
            }
        }
    }
}