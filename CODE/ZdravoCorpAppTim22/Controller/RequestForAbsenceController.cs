using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class RequestForAbsenceController : GenericController<RequestForAbsenceService, RequestForAbsence>
    {
        private static RequestForAbsenceController instance;
        private RequestForAbsenceController() : base(RequestForAbsenceService.Instance) { }
        public static RequestForAbsenceController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestForAbsenceController();
                }
                return instance;
            }
        }
        public bool validateDate(Interval requestInterval)
        {
            return RequestForAbsenceService.Instance.validateDate(requestInterval);
        }

        public bool areMultipleDoctorsOfSameTypeOnLeave(string specialization, Interval requestInterval)
        {
            return RequestForAbsenceService.Instance.areMultipleDoctorsOfSameTypeOnLeave(specialization, requestInterval);
        }

        public bool alreadyHasAnAppointment(Interval requestedInterval, Doctor requesting)
        {
            return RequestForAbsenceService.Instance.alreadyHasAnAppointment(requestedInterval, requesting);
        }

        public bool hasAlreadyRequestedAbsenceInSelectedPeriod(Interval requestedInterval, Doctor requesting)
        {
            return RequestForAbsenceService.Instance.hasAlreadyRequestedAbsenceInSelectedPeriod(requestedInterval, requesting);
        }

        public bool isRequestDenied(RequestForAbsence request)
        {
            return RequestForAbsenceService.Instance.isRequestDenied(request);
        }
    }
}
