using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class StuffMeetingRepository : GenericRepository<StuffMeeting>
    {
        public static string FileName = "StuffMeeting.json";
        private static StuffMeetingRepository instance;
        private StuffMeetingRepository() : base(FileName) { }
        public static StuffMeetingRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StuffMeetingRepository();
                }

                return instance;
            }
        }
    }
}
