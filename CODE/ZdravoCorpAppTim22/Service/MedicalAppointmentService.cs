using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.PackedObjects;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service.Generic;
namespace Service
{
    public class MedicalAppointmentService : GenericService<MedicalAppointmentRepository, MedicalAppointment>
    {
        private MedicalAppointmentService() : base(MedicalAppointmentRepository.Instance) { }
        public static MedicalAppointmentService Instance
        {
            get
            {
                return new MedicalAppointmentService();
            }
        }

        public List<MedicalAppointmentDTOforSuggestions> GetSuggestedMedicalAppointments(EnteredPreferences enteredPreferences)
        {
            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments = GetAvailableMedicalAppointments(enteredPreferences);
            availableMedicalAppointments = DefaultRearrange(enteredPreferences.EnteredDoctor, availableMedicalAppointments);
            availableMedicalAppointments = RearrangeByPriority(enteredPreferences, availableMedicalAppointments);
            return availableMedicalAppointments;
        }
        private static List<MedicalAppointmentDTOforSuggestions> GetAvailableMedicalAppointments(EnteredPreferences enteredPreferences)
        {
            DateTime appointmentTimeStart = new DateTime(enteredPreferences.EnteredDateTime.Year,
                enteredPreferences.EnteredDateTime.Month, enteredPreferences.EnteredDateTime.Day,
                Constants.Constants.WORK_DAY_START_TIME, 0, 0);

            DateTime workDayEndTime = new DateTime(enteredPreferences.EnteredDateTime.Year,
                enteredPreferences.EnteredDateTime.Month, enteredPreferences.EnteredDateTime.Day,
                Constants.Constants.WORK_DAY_END_TIME, 0, 0);

            List<Doctor> suggestedDoctors = FilterDoctors(enteredPreferences.EnteredAppointmentType);
            List<Room> suggestedRooms = FilterRooms(enteredPreferences.EnteredAppointmentType);

            SearchParametersForCreating searchParametersForCreating =
                new SearchParametersForCreating(enteredPreferences.EnteredPatient, enteredPreferences.EnteredAppointmentType,
                    appointmentTimeStart, SetDurationOfAppointment(enteredPreferences.EnteredAppointmentType), workDayEndTime, suggestedDoctors, suggestedRooms);

            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments =
                CreateMedicalAppointments(searchParametersForCreating);
            return availableMedicalAppointments;
        }

        public void MakeAppointment(MedicalAppointment medicalAppointment)
        {

            Create(medicalAppointment);
          

            PatientService.Instance.Update(medicalAppointment.Patient);
            DoctorService.Instance.Update(medicalAppointment.Doctor);
            RoomService.Instance.Update(medicalAppointment.Room);


        }

        private static List<Doctor> FilterDoctors(AppointmentType enteredAppointmentType)
        {
            List<Doctor> suggestedDoctors = DoctorService.Instance.GetAll();

            if (enteredAppointmentType == AppointmentType.Operation)
            {
                suggestedDoctors = (from doctor in suggestedDoctors
                                    let doctorSpecializationTemp = new DoctorSpecialization("Regular")
                                    where doctor.DoctorSpecialization.Name != doctorSpecializationTemp.Name
                                    select doctor).ToList();
            }

            return suggestedDoctors;
        }



        private static List<Room> FilterRooms(AppointmentType enteredAppointmentType)
        {
            List<Room> suggestedRooms = new List<Room>(RoomService.Instance.GetAll());

            if (enteredAppointmentType == AppointmentType.Operation)
            {
                suggestedRooms = suggestedRooms.Where(room => room.Type == RoomType.operation).ToList();
            }
            else
            {
                suggestedRooms = suggestedRooms.Where(room => room.Type == RoomType.examination).ToList();
            }

            return suggestedRooms;
        }
        private static int SetDurationOfAppointment(AppointmentType enteredAppointmentType)
        {
            int durationOfAppointment;
            switch (enteredAppointmentType)
            {
                case AppointmentType.Examination:
                    durationOfAppointment = Constants.Constants.DURATION_EXAMINATION;
                    break;
                case AppointmentType.Checkup:
                    durationOfAppointment = Constants.Constants.DURATION_CHECKUP;
                    break;
                case AppointmentType.Operation:
                    durationOfAppointment = Constants.Constants.DURATION_OPERATION;
                    break;
                default:
                    durationOfAppointment = Constants.Constants.DURATION_CHECKUP;
                    break;
            }

            return durationOfAppointment;
        }
        private static List<MedicalAppointmentDTOforSuggestions> CreateMedicalAppointments(SearchParametersForCreating searchParametersForCreating)
        {
            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments = new List<MedicalAppointmentDTOforSuggestions>();
            availableMedicalAppointments = PatientDoctorsRoomsAreAvailableCheck(searchParametersForCreating, availableMedicalAppointments);
            return availableMedicalAppointments;
        }

        private static List<MedicalAppointmentDTOforSuggestions> PatientDoctorsRoomsAreAvailableCheck(SearchParametersForCreating searchParametersForCreating, List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments)
        {
            Interval interval = new Interval();

            for (; CheckIfEndOfWorkHours(searchParametersForCreating); MoveToNextTimeSlot(searchParametersForCreating))
            {
                interval.Start = searchParametersForCreating.AppointmentTimeStart;
                interval.End = interval.Start.AddMinutes(searchParametersForCreating.DurationOfAppointment);

                if (searchParametersForCreating.EnteredPatient.IsAvailable(interval))
                {
                    foreach (var medicalAppointmentToAdd in from doctor in searchParametersForCreating.SuggestedDoctors
                                                            where doctor.IsAvailable(interval)
                                                            from room in searchParametersForCreating.SuggestedRooms
                                                            where room.IsAvailable(interval)
                                                            select new MedicalAppointmentDTOforSuggestions(-99, searchParametersForCreating.EnteredAppointmentType, interval, searchParametersForCreating.EnteredPatient,
                                                                 doctor, room))
                    {

                        availableMedicalAppointments.Add(medicalAppointmentToAdd);
                    }
                }
            }

            return availableMedicalAppointments;
        }




        private static void MoveToNextTimeSlot(SearchParametersForCreating searchParametersForCreating)
        {
            searchParametersForCreating.AppointmentTimeStart = searchParametersForCreating.AppointmentTimeStart.AddMinutes(Constants.Constants.NEXT_TIMESLOT_START_CHECK);
        }

        private static bool CheckIfEndOfWorkHours(SearchParametersForCreating searchParametersForCreating)
        {
            return searchParametersForCreating.AppointmentTimeStart.AddMinutes(searchParametersForCreating.DurationOfAppointment) <= searchParametersForCreating.WorkDayEndTime;
        }

        private static List<MedicalAppointmentDTOforSuggestions> DefaultRearrange(Doctor enteredDoctor, List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments)
        {
            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointmentsSortDefault = PreferredDoctorFirst(enteredDoctor, availableMedicalAppointments);

            foreach (MedicalAppointmentDTOforSuggestions medicalAppointmentStruct in availableMedicalAppointments)
            {
                if (medicalAppointmentStruct.Doctor.Id != enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(medicalAppointmentStruct);
                }
            }


            return availableMedicalAppointmentsSortDefault;
        }

        private static List<MedicalAppointmentDTOforSuggestions> PreferredDoctorFirst(Doctor enteredDoctor, List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments)
        {
            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointmentsSortDefault =
                new List<MedicalAppointmentDTOforSuggestions>();

            foreach (MedicalAppointmentDTOforSuggestions medicalAppointmentStruct in availableMedicalAppointments)
            {
                if (medicalAppointmentStruct.Doctor.Id == enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(medicalAppointmentStruct);
                }
            }

            return availableMedicalAppointmentsSortDefault;
        }

        private static List<MedicalAppointmentDTOforSuggestions> RearrangeByPriority(EnteredPreferences enteredPreferences, List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments)
        {
            if (!enteredPreferences.EnteredPriority.Equals("Lekar")) return availableMedicalAppointments;
            List<MedicalAppointmentDTOforSuggestions> availableMedicalAppointmentsSortDoctor =
                new List<MedicalAppointmentDTOforSuggestions>();

            foreach (MedicalAppointmentDTOforSuggestions item in availableMedicalAppointments)
            {
                if (item.Doctor.Id == enteredPreferences.EnteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDoctor.Add(item);
                }
            }

            if (availableMedicalAppointmentsSortDoctor.Count == 0)
            {
                EnteredPreferences enteredPreferencesRecursive = new EnteredPreferences(
                    enteredPreferences.EnteredPatient, enteredPreferences.EnteredDateTime.AddDays(1),
                    enteredPreferences.EnteredAppointmentType, enteredPreferences.EnteredPriority,
                    enteredPreferences.EnteredDoctor);

                availableMedicalAppointmentsSortDoctor =
                    Instance.GetSuggestedMedicalAppointments(enteredPreferencesRecursive);
            }


            return availableMedicalAppointmentsSortDoctor;


        }




        ///  ///  ///  ///  ///  ///  ///  /// ///  ///  ///  ///  ///  ///  ///  /// ///  ///  ///  ///  ///  ///  ///  /// 
        public List<Interval> GetNewMedicalAppointments(ForChangeMedicalAppointment forChangeMedicalAppointment)
        {

            DateTime appointmentTimeStart = new DateTime(forChangeMedicalAppointment.SelectedDateTime.Year, forChangeMedicalAppointment.SelectedDateTime.Month, forChangeMedicalAppointment.SelectedDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(forChangeMedicalAppointment.SelectedDateTime.Year, forChangeMedicalAppointment.SelectedDateTime.Month, forChangeMedicalAppointment.SelectedDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);

            int jumpToNextAppointmetnTime = Constants.Constants.NEXT_TIMESLOT_START_CHECK;
            int durationOfAppointment = SetDurationOfAppointment(forChangeMedicalAppointment.Type);

            SearchParametersForChanging searchParametersForChanging = new SearchParametersForChanging(forChangeMedicalAppointment, appointmentTimeStart, durationOfAppointment, workDayEndTime, jumpToNextAppointmetnTime);

            var availableMedicalAppointments = CreateMedicalAppointmentForChange(searchParametersForChanging);

            return availableMedicalAppointments;
        }

        private static List<Interval> CreateMedicalAppointmentForChange(SearchParametersForChanging searchParametersForChanging)
        {
            List<Interval> availableMedicalAppointments = new List<Interval>();

            PatientDoctorRoomAreAvailableCheck(searchParametersForChanging, availableMedicalAppointments);

            return availableMedicalAppointments;
        }

        private static void PatientDoctorRoomAreAvailableCheck(SearchParametersForChanging searchParametersForChanging,
            List<Interval> availableMedicalAppointments)
        {
            Interval interval = new Interval();

            for (;
                 searchParametersForChanging.AppointmentTimeStart.AddMinutes(searchParametersForChanging
                     .DurationOfAppointment) <= searchParametersForChanging.WorkDayEndTime;
                 searchParametersForChanging.AppointmentTimeStart =
                     searchParametersForChanging.AppointmentTimeStart.AddMinutes(searchParametersForChanging
                         .JumpToNextAppointmetnTime))
            {
                interval.Start = searchParametersForChanging.AppointmentTimeStart;
                interval.End = interval.Start.AddMinutes(searchParametersForChanging.DurationOfAppointment);


                if (IsAvailablePatientDoctorRoom(searchParametersForChanging, interval))
                {
                    availableMedicalAppointments.Add(interval);
                }
            }
        }

        private static bool IsAvailablePatientDoctorRoom(SearchParametersForChanging searchParametersForChanging, Interval interval)
        {
            return searchParametersForChanging.ForChangeMedicalAppointment.EnteredPatient.IsAvailable(interval) &&
                   searchParametersForChanging.ForChangeMedicalAppointment.Doctor.IsAvailable(interval) &&
                   searchParametersForChanging.ForChangeMedicalAppointment.Room.IsAvailable(interval);
        }


        // SEKRETAROVO
        public ObservableCollection<MedicalAppointmentDTOforSuggestions> GetSuggestedMedicalAppointments(AppointmentPreferences appointmentPreferences)
        {
            ObservableCollection<MedicalAppointmentDTOforSuggestions> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentDTOforSuggestions>();

            int hour = Constants.Constants.WORK_DAY_START_TIME;
            int min = 0;
            //Time fixing
            if (appointmentPreferences.enteredDateTime.Date == DateTime.Today.Date)
            {
                Console.WriteLine("app " + appointmentPreferences.enteredDateTime.Date + "  today " + DateTime.Today.Date);
                if (hour <= DateTime.Now.Hour)
                {
                    hour = DateTime.Now.Hour;
                }

                if (DateTime.Now.Minute > 45)
                {
                    hour++;
                    min = 0;
                }
                else if (DateTime.Now.Minute > 30)
                {
                    min = 45;

                }
                else if (DateTime.Now.Minute > 15)
                {
                    min = 30;

                }
                else
                {
                    min = 15;
                }
            }

            DateTime appointmentTimeStart = new DateTime(appointmentPreferences.enteredDateTime.Year, appointmentPreferences.enteredDateTime.Month, appointmentPreferences.enteredDateTime.Day, hour, min, 0);
            DateTime workDayEndTime = new DateTime(appointmentPreferences.enteredDateTime.Year, appointmentPreferences.enteredDateTime.Month, appointmentPreferences.enteredDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);


            int jumpToNextAppointmetnTime = Constants.Constants.NEXT_TIMESLOT_START_CHECK;
            int durationOfAppointment = Constants.Constants.DURATION_CHECKUP;

            if (appointmentPreferences.enteredAppointmentType == AppointmentType.Examination)
            {
                durationOfAppointment = Constants.Constants.DURATION_EXAMINATION;
            }
            else if (appointmentPreferences.enteredAppointmentType == AppointmentType.Operation)
            {
                durationOfAppointment = Constants.Constants.DURATION_OPERATION;
            }

            List<Doctor> suggestedDoctors = new List<Doctor>(DoctorService.Instance.GetAll());
            List<Room> suggestedRooms = new List<Room>(RoomService.Instance.GetAll());

            if (appointmentPreferences.enteredAppointmentType == AppointmentType.Operation)
            {
                List<Doctor> temporaryDoctors = new List<Doctor>();

                foreach (Doctor doctor in suggestedDoctors)
                {
                    if (doctor.DoctorSpecialization == appointmentPreferences.enteredDoctor.DoctorSpecialization)
                    {
                        temporaryDoctors.Add(doctor);
                    }
                }
                suggestedDoctors = temporaryDoctors;

                List<Room> temporaryRoomsOperation = new List<Room>();

                foreach (Room room in suggestedRooms)
                {
                    if (room.Type == RoomType.operation)
                    {
                        temporaryRoomsOperation.Add(room);
                    }
                }
                suggestedRooms = temporaryRoomsOperation;


            }
            else
            {
                List<Room> temporaryRooms = new List<Room>();

                foreach (Room room in suggestedRooms)
                {
                    if (room.Type == RoomType.examination)
                    {
                        temporaryRooms.Add(room);
                    }
                }
                suggestedRooms = temporaryRooms;
            }

            Interval interval = new Interval();
            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                DateTime appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);

                interval.Start = appointmentTimeStart;
                interval.End = appointmentTimeEnd;

                if (appointmentPreferences.enteredPatient.IsAvailable(interval))
                {
                    foreach (Doctor doctor in suggestedDoctors)
                    {
                        if (doctor.IsAvailable(interval))
                        {
                            foreach (Room room in suggestedRooms)
                            {
                                if (room.IsAvailable(interval))
                                {
                                    MedicalAppointmentDTOforSuggestions medicalAppointmentToAdd = new MedicalAppointmentDTOforSuggestions(-1, appointmentPreferences.enteredAppointmentType, interval, appointmentPreferences.enteredPatient, doctor, room);
                                    availableMedicalAppointments.Add(medicalAppointmentToAdd);

                                }
                            }
                        }
                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);

            }

            ObservableCollection<MedicalAppointmentDTOforSuggestions> availableMedicalAppointmentsSortDefault = new ObservableCollection<MedicalAppointmentDTOforSuggestions>();

            foreach (MedicalAppointmentDTOforSuggestions item in availableMedicalAppointments)
            {
                if (item.Doctor.Id == appointmentPreferences.enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            foreach (MedicalAppointmentDTOforSuggestions item in availableMedicalAppointments)
            {
                if (!(item.Doctor.Id == appointmentPreferences.enteredDoctor.Id))
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            availableMedicalAppointments = new ObservableCollection<MedicalAppointmentDTOforSuggestions>(availableMedicalAppointmentsSortDefault.OrderBy(i => i.Interval.Start));
            if (appointmentPreferences.enteredPriority == AppointemntPriorityEnum.Time)
            {
                return availableMedicalAppointments;
            }

            if (appointmentPreferences.enteredPriority == AppointemntPriorityEnum.Doctor)
            {
                ObservableCollection<MedicalAppointmentDTOforSuggestions> availableMedicalAppointmentsSortDoctor = new ObservableCollection<MedicalAppointmentDTOforSuggestions>();

                foreach (MedicalAppointmentDTOforSuggestions item in availableMedicalAppointments)
                {
                    if (item.Doctor.Id == appointmentPreferences.enteredDoctor.Id)
                    {
                        availableMedicalAppointmentsSortDoctor.Add(item);
                    }
                }

                if (availableMedicalAppointmentsSortDoctor.Count == 0)
                {
                    AppointmentPreferences appointmentPreferencesTemp = appointmentPreferences;
                    appointmentPreferencesTemp.enteredDateTime.AddDays(1);
                    availableMedicalAppointmentsSortDoctor = Instance.GetSuggestedMedicalAppointments(appointmentPreferencesTemp);
                }
                availableMedicalAppointments = availableMedicalAppointmentsSortDoctor;

            }
            return availableMedicalAppointments;
        }
        public ObservableCollection<MedicalAppointment> GetUnavailableMedicalAppointmentsInNextHour(AppointmentPreferences appointmentPreferences)
        {
            ObservableCollection<MedicalAppointment> medicalAppointmentsAll = new ObservableCollection<MedicalAppointment>(GetAll());
            ObservableCollection<MedicalAppointment> medicalAppointments = new ObservableCollection<MedicalAppointment>();
            for (int i = 0; i < medicalAppointmentsAll.Count; i++)
            {
                if (medicalAppointmentsAll[i].Interval.Start >= DateTime.Now.AddHours(1) || medicalAppointmentsAll[i].Interval.Start <= DateTime.Now)
                {
                    continue;
                }

                if (medicalAppointmentsAll[i].Interval.End <= DateTime.Now)
                {
                    continue;
                }

                if (medicalAppointmentsAll[i].isUrgent)
                {
                    continue;
                }

                if (medicalAppointmentsAll[i].Doctor.DoctorSpecialization != appointmentPreferences.enteredDoctor.DoctorSpecialization)
                {
                    continue;
                }

                medicalAppointments.Add(medicalAppointmentsAll[i]);
            }

            return medicalAppointments;
        }
    }
}