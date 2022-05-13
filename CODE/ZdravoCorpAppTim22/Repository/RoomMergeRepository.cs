using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class RoomMergeRepository : GenericRepository<RoomMerge>
    {
        public static string FileName = "RoomMergeData.json";
        private static RoomMergeRepository instance;
        private RoomMergeRepository() : base(FileName) { }
        public static RoomMergeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomMergeRepository();
                }

                return instance;
            }
        }
    }
}
