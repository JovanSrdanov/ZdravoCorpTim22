using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public List<MedicalAppointment> GetSuggestedMedicalAppointments(EnteredPreferences enteredPreferences)
        {
            List<MedicalAppointment> availableMedicalAppointments = GetAvailableMedicalAppointments(enteredPreferences);
            availableMedicalAppointments= DefaultRearrange(enteredPreferences.EnteredDoctor, availableMedicalAppointments);
            availableMedicalAppointments = RearrangeByPriority(enteredPreferences, availableMedicalAppointments);
            return availableMedicalAppointments;
        }
        private static List<MedicalAppointment> GetAvailableMedicalAppointments(EnteredPreferences enteredPreferences)
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

            List<MedicalAppointment> availableMedicalAppointments =
                CreateMedicalAppointments(searchParametersForCreating);
            return availableMedicalAppointments;
        }

        public void MakeAppointment(MedicalAppointment medicalAppointment)
        {
            Instance.Create(medicalAppointment);

            medicalAppointment.patient.MedicalAppointment.Add(medicalAppointment);
            medicalAppointment.Doctor.MedicalAppointment.Add(medicalAppointment);
            medicalAppointment.Room.MedicalAppointment.Add(medicalAppointment);

            PatientService.Instance.Update(medicalAppointment.patient);
            DoctorService.Instance.Update(medicalAppointment.doctor);
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
        private static List<MedicalAppointment> CreateMedicalAppointments(SearchParametersForCreating searchParametersForCreating)
        {
            List<MedicalAppointment> availableMedicalAppointments = new List<MedicalAppointment>();
            PatientDoctorsRoomsAreAvailableCheck(searchParametersForCreating, availableMedicalAppointments);
            return availableMedicalAppointments;
        }

        private static void PatientDoctorsRoomsAreAvailableCheck(SearchParametersForCreating searchParametersForCreating, List<MedicalAppointment> availableMedicalAppointments)
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
                                                            select new MedicalAppointment(-1, searchParametersForCreating.EnteredAppointmentType, interval, room,
                                                                searchParametersForCreating.EnteredPatient, doctor))
                    {
                        availableMedicalAppointments.Add(medicalAppointmentToAdd);
                    }
                }
            }
        }




        private static void MoveToNextTimeSlot(SearchParametersForCreating searchParametersForCreating)
        {
            searchParametersForCreating.AppointmentTimeStart = searchParametersForCreating.AppointmentTimeStart.AddMinutes(Constants.Constants.NEXT_TIMESLOT_START_CHECK);
        }

        private static bool CheckIfEndOfWorkHours(SearchParametersForCreating searchParametersForCreating)
        {
            return searchParametersForCreating.AppointmentTimeStart.AddMinutes(searchParametersForCreating.DurationOfAppointment) <= searchParametersForCreating.WorkDayEndTime;
        }

        private static List<MedicalAppointment> DefaultRearrange(Doctor enteredDoctor, List<MedicalAppointment> availableMedicalAppointments)
        {
            List<MedicalAppointment> availableMedicalAppointmentsSortDefault = PreferredDoctorFirst(enteredDoctor, availableMedicalAppointments);

            foreach (MedicalAppointment medicalAppointmentStruct in availableMedicalAppointments)
            {
                if (medicalAppointmentStruct.Doctor.Id != enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(medicalAppointmentStruct);
                }
            }


            return availableMedicalAppointmentsSortDefault;
        }

        private static List<MedicalAppointment> PreferredDoctorFirst(Doctor enteredDoctor, List<MedicalAppointment> availableMedicalAppointments)
        {
            List<MedicalAppointment> availableMedicalAppointmentsSortDefault =
                new List<MedicalAppointment>();

            foreach (MedicalAppointment medicalAppointmentStruct in availableMedicalAppointments)
            {
                if (medicalAppointmentStruct.Doctor.Id == enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(medicalAppointmentStruct);
                }
            }

            return availableMedicalAppointmentsSortDefault;
        }

        private static List<MedicalAppointment> RearrangeByPriority(EnteredPreferences enteredPreferences, List<MedicalAppointment> availableMedicalAppointments)
        {
            if (!enteredPreferences.EnteredPriority.Equals("Lekar")) return availableMedicalAppointments;
            List<MedicalAppointment> availableMedicalAppointmentsSortDoctor =
                new List<MedicalAppointment>();

            foreach (MedicalAppointment item in availableMedicalAppointments)
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
        public ObservableCollection<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(AppointmentPreferences appointmentPreferences)
        {
            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>();

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
                                    MedicalAppointmentStruct medicalAppointmentToAdd = new MedicalAppointmentStruct(-1, appointmentPreferences.enteredAppointmentType, interval, appointmentPreferences.enteredPatient, doctor, room);
                                    availableMedicalAppointments.Add(medicalAppointmentToAdd);

                                }
                            }
                        }
                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);

            }

            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointmentsSortDefault = new ObservableCollection<MedicalAppointmentStruct>();

            foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
            {
                if (item.Doctor.Id == appointmentPreferences.enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
            {
                if (!(item.Doctor.Id == appointmentPreferences.enteredDoctor.Id))
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>(availableMedicalAppointmentsSortDefault.OrderBy(i => i.Interval.Start));
            if (appointmentPreferences.enteredPriority == AppointemntPriorityEnum.Time)
            {
                return availableMedicalAppointments;
            }

            if (appointmentPreferences.enteredPriority == AppointemntPriorityEnum.Doctor)
            {
                ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointmentsSortDoctor = new ObservableCollection<MedicalAppointmentStruct>();

                foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
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