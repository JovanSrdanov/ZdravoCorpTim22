using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RoomAppointmentViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        public RoomAppointmentViewModel(Room room, DateTime date)
        {
            Appointments = new ObservableCollection<Appointment>(CreateFullList(room, date));
        }
        public RoomAppointmentViewModel(Room room, DateTime date, DateTime startDate)
        {
            List<Appointment> list = CreateFullList(room, date);
            if(DateTime.Compare(date.Date, startDate.Date) > 0)
            {
                Appointments = new ObservableCollection<Appointment>(new List<Appointment>
                {
                    list[0]
                });
            }
            else
            {
                foreach(Appointment app in list)
                {
                    if(DateTime.Compare(app.Interval.Start, startDate) <= 0 && DateTime.Compare(app.Interval.End, startDate) > 0)
                    {
                        Interval interval = new Interval()
                        {
                            Start = startDate,
                            End = app.Interval.End
                        };
                        Appointments = new ObservableCollection<Appointment>(new List<Appointment>
                        {
                            new Appointment()
                            {
                                Interval = interval,
                                Type = app.Type,
                            }
                        });
                        break;
                    }
                }
            }
        }

        private List<Appointment> GetAppointmentsForDay<T>(List<T> list, DateTime date) where T : IHasInterval
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (T obj in list)
            {
                if (obj.Interval.Start.Date == date.Date && obj.Interval.End.Date == date.Date)
                {
                    Appointment app = new Appointment(obj.Interval.Start, obj.Interval.End, RoomAppointmentType.RenovationAppointment);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date < date.Date && obj.Interval.End.Date == date.Date)
                {
                    Appointment app = new Appointment(date.Date, obj.Interval.End, RoomAppointmentType.RenovationAppointment);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date == date.Date && obj.Interval.End.Date > date.Date)
                {
                    Appointment app = new Appointment(obj.Interval.Start, date.Date.AddHours(23).AddMinutes(59), RoomAppointmentType.RenovationAppointment);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date < date.Date && obj.Interval.End.Date > date.Date)
                {
                    Appointment app = new Appointment(date.Date, date.Date.AddHours(23).AddMinutes(59), RoomAppointmentType.RenovationAppointment);
                    appointmentList.Add(app);
                }
            }
            return appointmentList;
        }
        private List<Appointment> GetAllAppointmentsForDay(Room room, DateTime date)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (MedicalAppointment medApp in room.MedicalAppointment)
            {
                if (date.Date == medApp.MedicalAppointmentStartDateTime.Date)
                {
                    Appointment app = new Appointment(medApp.MedicalAppointmentStartDateTime, medApp.MedicalAppointmentEndDateTime, RoomAppointmentType.DoctorAppointment);
                    appointmentList.Add(app);
                }
            }
            appointmentList.AddRange(GetAppointmentsForDay(room.Renovations, date));
            appointmentList.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            return appointmentList;
        }
        private List<Appointment> CreateFullList(Room room, DateTime date)
        {
            List<Appointment> appointmentList = GetAllAppointmentsForDay(room, date);

            if (appointmentList.Count == 0)
            {
                Appointment app = new Appointment(date.Date, date.Date.AddHours(23).AddMinutes(59), RoomAppointmentType.Free);
                return new List<Appointment>() { app };
            }

            List<Appointment> freeList = new List<Appointment>();
            for (int i = 0; i < appointmentList.Count - 1; i++)
            {
                if (DateTime.Compare(appointmentList[i].Interval.End, appointmentList[i + 1].Interval.Start) < 0)
                {
                    Appointment app = new Appointment(appointmentList[i].Interval.End, appointmentList[i + 1].Interval.Start, RoomAppointmentType.Free);
                    freeList.Add(app);
                }
            }
            appointmentList.AddRange(freeList);
            appointmentList.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            if (appointmentList.Count > 0)
            {
                DateTime first = appointmentList[0].Interval.Start;
                if (DateTime.Compare(first, first.Date) > 0)
                {
                    Appointment app = new Appointment(first.Date, first, RoomAppointmentType.Free);
                    appointmentList.Insert(0, app);
                }
                DateTime last = appointmentList[appointmentList.Count - 1].Interval.End;
                if (DateTime.Compare(last, last.Date.AddHours(23).AddMinutes(59)) < 0)
                {
                    Appointment app = new Appointment(last, last.Date.AddHours(23).AddMinutes(59), RoomAppointmentType.Free);
                    appointmentList.Insert(appointmentList.Count, app);
                }
            }
            return appointmentList;
        }


        public static DateTime GetLatestAvailableTime(Room room, DateTime startDate)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (MedicalAppointment medApp in room.MedicalAppointment)
            {
                if (DateTime.Compare(startDate, medApp.MedicalAppointmentEndDateTime) < 0)
                {
                    Appointment app = new Appointment(medApp.MedicalAppointmentStartDateTime, medApp.MedicalAppointmentEndDateTime, RoomAppointmentType.DoctorAppointment);
                    appointmentList.Add(app);
                }
            }
            foreach (Renovation ren in room.Renovations)
            {
                if (DateTime.Compare(startDate, ren.Interval.End) < 0)
                {
                    Appointment app = new Appointment(ren.Interval.Start, ren.Interval.End, RoomAppointmentType.RenovationAppointment);
                    appointmentList.Add(app);
                }
            }
            appointmentList.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            if(appointmentList.Count > 0)
            {
                return appointmentList[0].Interval.Start;
            }
            else
            {
                return startDate;
            }
        }
        
    }
    
    public struct Appointment : IHasInterval
    {
        public Interval Interval { get; set; }
        public RoomAppointmentType Type { get; set; }
        public Appointment(Interval interval, RoomAppointmentType type){
            Interval = interval;
            Type = type;
        }

        public Appointment(DateTime start, DateTime end, RoomAppointmentType type)
        {
            Interval = new Interval()
            {
                Start = start,
                End = end
            };
            Type = type;
        }
    }
    
    public enum RoomAppointmentType
    {
        DoctorAppointment,
        RenovationAppointment,
        EquipmentTransferAppointment,
        Free
    }
}
