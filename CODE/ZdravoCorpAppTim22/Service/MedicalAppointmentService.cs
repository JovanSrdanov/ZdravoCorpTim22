using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


            int jumpToNextAppointmetnTime = 15;
            int durationOfAppointment = 15;

            if (enteredAppointmentType == AppointmentType.examination)
            {
                durationOfAppointment = 30;
            }

            if (enteredAppointmentType == AppointmentType.operation)
            {
                durationOfAppointment = 60;
            }

            List<Doctor> suggestedDoctors = new List<Doctor>(DoctorService.Instance.GetAll());
            List<Room> suggestetRooms = new List<Room>(RoomService.Instance.GetAll());

            if (enteredAppointmentType == AppointmentType.operation)
            {
                List<Doctor> temporaryDoctors = new List<Doctor>();

                foreach (Doctor doctor in suggestedDoctors)
                {
                    if (doctor.DoctorType == DoctorSpecialisationType.specialist)
                        temporaryDoctors.Add(doctor);

                }
                suggestedDoctors = temporaryDoctors;

                List<Room> temporaryRooms = new List<Room>();

                foreach (Room room in suggestetRooms)
                {

                    if (room.Type == RoomType.operation)
                        temporaryRooms.Add(room);

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

        public ObservableCollection<MedicalAppointmentStruct> GetNewMedicalAppointments(Doctor doctor, Room room, Patient enteredPatient, DateTime selectedDateTime, AppointmentType type)
        {
            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);


            int jumpToNextAppointmetnTime = 15;
            int durationOfAppointment = 15;

            if (type == AppointmentType.examination)
            {
                durationOfAppointment = 30;
            }

            if (type == AppointmentType.operation)
            {
                durationOfAppointment = 60;

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