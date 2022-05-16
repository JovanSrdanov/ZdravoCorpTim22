using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class RequestForAbsenceService : GenericService<RequestForAbsenceRepository, RequestForAbsence>
    {
        private static RequestForAbsenceService instance;
        private RequestForAbsenceService() : base(RequestForAbsenceRepository.Instance) { }
        public static RequestForAbsenceService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestForAbsenceService();
                }
                return instance;
            }
        }
        public bool validateDate(Interval requestInterval)
        {
            bool validDate = true;      //ako je pocetak posle kraja ili se pocetak odsustva trazi na manje od 2 dana razlike od trenutnog dana
            if (requestInterval.Start > requestInterval.End || (requestInterval.Start.Subtract(DateTime.Now).TotalDays < 2))
                validDate = false;
            return validDate;
        }

        public bool areMultipleDoctorsOfSameTypeOnLeave(string specialization, Interval requestInterval)
        {
            int numOfDoctors = 0;
            bool returnValue = false;
            foreach (RequestForAbsence request in RequestForAbsenceRepository.Instance.GetAll())
            {
                if(request.Doctor.DoctorSpecialization.Name.Equals(specialization) && checkIfCrossedDates(requestInterval, request)
                    && (request.RequestState != RequestState.rejected))     //ako je doktor iste specijalizacije
                    numOfDoctors++;                                         //vec na odustvu u trazenom periodu i njegov zahtev nije odbijen
                if(numOfDoctors > 1)
                {
                    returnValue = true;
                    break;
                }
            }
            return returnValue;
        }
        private bool checkIfCrossedDates(Interval requestedInterval, RequestForAbsence request)
        {
            bool returnValue = true;
            if (requestedInterval.Start > request.AbsenceInterval.End || requestedInterval.End < request.AbsenceInterval.Start)
            {
                returnValue = false;
            }
            return returnValue;
        }

        public bool alreadyHasAnAppointment(Interval requestedInterval, Doctor requesting)
        {
            bool returnValue = false;
            List<MedicalAppointment> requestingDoctorAppointments = requesting.MedicalAppointment;
            foreach (MedicalAppointment appointment in requestingDoctorAppointments)
            {
                if (!(requestedInterval.Start > appointment.Interval.End || requestedInterval.End < appointment.Interval.Start))
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }

        public bool hasAlreadyRequestedAbsenceInSelectedPeriod(Interval requestedInterval, Doctor requesting)
        {
            bool returnValue = false;
            List<RequestForAbsence> requests = requesting.RequestForAbsence;
            foreach (RequestForAbsence request in requests)
            {
                if (!(requestedInterval.Start > request.AbsenceInterval.End || requestedInterval.End < request.AbsenceInterval.Start)
                    && (request.RequestState != RequestState.rejected))
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
    }
}
