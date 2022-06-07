using Service;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.Controller
{
    public class StuffMeetingController : GenericController<StuffMeetingService, StuffMeeting>
    {
        private static StuffMeetingController instance;
        private StuffMeetingController() : base(StuffMeetingService.Instance) { }
        public static StuffMeetingController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StuffMeetingController();
                }

                return instance;
            }
        }
    }
}
