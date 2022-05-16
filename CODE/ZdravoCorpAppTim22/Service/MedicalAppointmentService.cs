using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service.Generic;
namespace Service
{
    public class MedicalAppointmentService : GenericService<MedicalAppointmentRepository, MedicalAppointment>
    {
        private static MedicalAppointmentService instance;
        private MedicalAppointmentService() : base(MedicalAppointmentRepository.Instance) { }
        public static MedicalAppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentService();
                }

                return instance;
            }
        }
        public ObservableCollection<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(Patient enteredPatient, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority, Doctor enteredDoctor)
        {
            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);

            int jumpToNextAppointmetnTime = Constants.Constants.NEXT_TIMESLOT_START_CHECK;
            int durationOfAppointment = Constants.Constants.DURATION_CHECKUP;

            if (enteredAppointmentType == AppointmentType.Examination)
            {
                durationOfAppointment = Constants.Constants.DURATION_EXAMINATION;
            }

            if (enteredAppointmentType == AppointmentType.Operation)
            {
                durationOfAppointment = Constants.Constants.DURATION_OPERATION;
            }

           
            List<Doctor> suggestedDoctors = new List<Doctor>(DoctorService.Instance.GetAll());
            List<Room> suggestetRooms = new List<Room>(RoomService.Instance.GetAll());

            if (enteredAppointmentType == AppointmentType.Operation)
            {
                List<Doctor> temporaryDoctors = new List<Doctor>();

                foreach (Doctor doctor in suggestedDoctors)
                {
                    if (doctor.DoctorType == DoctorSpecialisationType.specialist)
                    {
                        temporaryDoctors.Add(doctor);
                    }
                }
                suggestedDoctors = temporaryDoctors;

                List<Room> temporaryRoomsOperation = new List<Room>();

                foreach (Room room in suggestetRooms)
                {
                    if (room.Type == RoomType.operation)
                    {
                        temporaryRoomsOperation.Add(room);
                    }
                }
                suggestetRooms = temporaryRoomsOperation;


            }
            else
            {
                List<Room> temporaryRooms = new List<Room>();

                foreach (Room room in suggestetRooms)
                {
                    if (room.Type == RoomType.examination)
                    {
                        temporaryRooms.Add(room);
                    }
                }
                suggestetRooms = temporaryRooms;
            }

            Interval interval = new Interval();
            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                DateTime appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);

                interval.Start = appointmentTimeStart;
                interval.End = appointmentTimeEnd;

                if (enteredPatient.IsAvailable(interval))
                {
                    foreach (Doctor doctor in suggestedDoctors)
                    {
                        if (doctor.IsAvailable(interval))
                        {
                            foreach (Room room in suggestetRooms)
                            {
                                if (room.IsAvailable(interval))
                                {
                                    MedicalAppointmentStruct medicalAppointmentToAdd = new MedicalAppointmentStruct(-1, enteredAppointmentType, interval, enteredPatient, doctor, room);
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
                if (item.Doctor.Id == enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
            {
                if (!(item.Doctor.Id == enteredDoctor.Id))
                {
                    availableMedicalAppointmentsSortDefault.Add(item);
                }
            }

            availableMedicalAppointments = availableMedicalAppointmentsSortDefault;

            if (enteredPriority.Equals("Lekar"))
            {
                ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointmentsSortDoctor = new ObservableCollection<MedicalAppointmentStruct>();

                foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
                {
                    if (item.Doctor.Id == enteredDoctor.Id)
                    {
                        availableMedicalAppointmentsSortDoctor.Add(item);
                    }
                }

                if (availableMedicalAppointmentsSortDoctor.Count == 0)
                {
                    availableMedicalAppointmentsSortDoctor = Instance.GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime.AddDays(1), enteredAppointmentType, enteredPriority, enteredDoctor);
                }
                availableMedicalAppointments = availableMedicalAppointmentsSortDoctor;

            }
            return availableMedicalAppointments;
        }

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
                    if (doctor.DoctorType == DoctorSpecialisationType.specialist)
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

        public ObservableCollection<MedicalAppointmentStruct> GetNewMedicalAppointments(Doctor doctor, Room room, Patient enteredPatient, DateTime selectedDateTime, AppointmentType type)
        {
            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);

            int jumpToNextAppointmetnTime = Constants.Constants.NEXT_TIMESLOT_START_CHECK;
            int durationOfAppointment = Constants.Constants.DURATION_CHECKUP;

            if (type == AppointmentType.Examination)
            {
                durationOfAppointment = Constants.Constants.DURATION_EXAMINATION;
            }

            if (type == AppointmentType.Operation)
            {
                durationOfAppointment = Constants.Constants.DURATION_OPERATION;
            }

            Interval interval = new Interval();
            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                DateTime appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);
                interval.Start = appointmentTimeStart;
                interval.End = appointmentTimeEnd;

                if (enteredPatient.IsAvailable(interval))
                {
                    if (doctor.IsAvailable(interval))
                    {
                        if (room.IsAvailable(interval))
                        {
                            MedicalAppointmentStruct medicalAppointmentStructToAdd = new MedicalAppointmentStruct(1, type, interval, enteredPatient, doctor, room);
                            availableMedicalAppointments.Add(medicalAppointmentStructToAdd);

                        }
                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);
            }

            return availableMedicalAppointments;
        }
    }
}