using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class RoomDivergeRepository : GenericRepository<RoomDiverge>
    {
        public static string FileName = "RoomDivergeData.json";
        private static RoomDivergeRepository instance;
        private RoomDivergeRepository() : base(FileName) { }
        public static RoomDivergeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomDivergeRepository();

                }

                return instance;
            }
        }
    }
}
