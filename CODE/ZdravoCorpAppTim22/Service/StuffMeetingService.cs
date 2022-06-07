using Repository;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class StuffMeetingService : GenericService<StuffMeetingRepository, StuffMeeting>
    {
        private StuffMeetingService() : base(StuffMeetingRepository.Instance) { }
        public static StuffMeetingService Instance
        {
            get
            {
                return new StuffMeetingService();
            }
        }
    }
}
