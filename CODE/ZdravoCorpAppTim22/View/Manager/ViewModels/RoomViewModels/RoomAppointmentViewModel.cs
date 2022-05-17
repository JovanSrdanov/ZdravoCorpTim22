using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomAppointmentViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        public RoomAppointmentViewModel(Room room, DateTime date)
        {
            Appointments = new ObservableCollection<Appointment>(CreateFullList(room, date));
        }
        public RoomAppointmentViewModel(Room room1, Room room2, DateTime date)
        {
            
            Appointments = new ObservableCollection<Appointment>(CreateMergedList(room1, room2, date));
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
        public RoomAppointmentViewModel(Room room1, Room room2, DateTime date, DateTime startDate)
        {
            List<Appointment> list = CreateMergedList(room1, room2, date);
            if (DateTime.Compare(date.Date, startDate.Date) > 0)
            {
                Appointments = new ObservableCollection<Appointment>(new List<Appointment>
                {
                    list[0]
                });
            }
            else
            {
                foreach (Appointment app in list)
                {
                    if (DateTime.Compare(app.Interval.Start, startDate) <= 0 && DateTime.Compare(app.Interval.End, startDate) > 0)
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

        private List<Appointment> GetAppointmentsForDay<T>(List<T> list, DateTime date, RoomAppointmentType appointmentType) where T : IHasInterval
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (T obj in list)
            {
                if (obj.Interval.Start.Date == date.Date && obj.Interval.End.Date == date.Date)
                {
                    Appointment app = new Appointment(obj.Interval.Start, obj.Interval.End, appointmentType);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date < date.Date && obj.Interval.End.Date == date.Date)
                {
                    Appointment app = new Appointment(date.Date, obj.Interval.End, appointmentType);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date == date.Date && obj.Interval.End.Date > date.Date)
                {
                    Appointment app = new Appointment(obj.Interval.Start, date.Date.AddHours(23).AddMinutes(59), appointmentType);
                    appointmentList.Add(app);
                }
                else if (obj.Interval.Start.Date < date.Date && obj.Interval.End.Date > date.Date)
                {
                    Appointment app = new Appointment(date.Date, date.Date.AddHours(23).AddMinutes(59), appointmentType);
                    appointmentList.Add(app);
                }
            }
            return appointmentList;
        }
        private List<Appointment> GetAllAppointmentsForDay(Room room, DateTime date)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            appointmentList.AddRange(GetAppointmentsForDay(room.MedicalAppointment, date, RoomAppointmentType.DoctorAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.Renovations, date, RoomAppointmentType.RenovationAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.RelocationSources, date, RoomAppointmentType.EquipmentRelocationAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.RelocationDestinations, date, RoomAppointmentType.EquipmentRelocationAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.MergesWhereFirst, date, RoomAppointmentType.RoomMergeAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.MergesWhereSecond, date, RoomAppointmentType.RoomMergeAppointment));
            appointmentList.AddRange(GetAppointmentsForDay(room.Diverges, date, RoomAppointmentType.RoomDivergeAppointment));
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
        private List<Appointment> CreateMergedList(Room room1, Room room2, DateTime date)
        {
            List<Appointment> list1 = CreateFullList(room1, date);
            List<Appointment> list2 = CreateFullList(room2, date);
            List<Appointment> list = new List<Appointment>();
            foreach (Appointment item1 in list1)
            {
                if (item1.Type != RoomAppointmentType.Free) continue;
                foreach (Appointment item2 in list2)
                {
                    if (item2.Type != RoomAppointmentType.Free) continue;
                    Interval i1 = item1.Interval;
                    Interval i2 = item2.Interval;

                    if (i1.Start >= i2.Start && i1.End <= i2.End)
                    {
                        list.Add(item1);
                    }
                    else if (i2.Start >= i1.Start && i2.End <= i1.End)
                    {
                        list.Add(item2);
                    }
                    else if (i1.Start >= i2.Start && i1.Start <= i2.End && i2.End <= i1.End)
                    {
                        if(room1.IsAvailable(i1.Start, i2.End) && room2.IsAvailable(i1.Start, i2.End))
                        {
                            list.Add(new Appointment(i1.Start, i2.End, RoomAppointmentType.Free));
                        }
                    }
                    else if (i2.Start >= i1.Start && i2.Start <= i1.End && i1.End <= i2.End)
                    {
                        if (room1.IsAvailable(i2.Start, i1.End) && room2.IsAvailable(i2.Start, i1.End))
                        {
                            list.Add(new Appointment(i2.Start, i1.End, RoomAppointmentType.Free));
                        }
                    }
                }
            }
            list.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            return list;
        }
        public static DateTime GetLatestAvailableTime(Room room, DateTime startDate)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            appointmentList.AddRange(GetAllNotExpired(room.MedicalAppointment, startDate, RoomAppointmentType.DoctorAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.Renovations, startDate, RoomAppointmentType.RenovationAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.RelocationSources, startDate, RoomAppointmentType.EquipmentRelocationAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.RelocationDestinations, startDate, RoomAppointmentType.EquipmentRelocationAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.MergesWhereFirst, startDate, RoomAppointmentType.RoomMergeAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.MergesWhereSecond, startDate, RoomAppointmentType.RoomMergeAppointment));
            appointmentList.AddRange(GetAllNotExpired(room.Diverges, startDate, RoomAppointmentType.RoomDivergeAppointment));
            appointmentList.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            return appointmentList.Count > 0 ? appointmentList[0].Interval.Start : startDate;
        }
        public static DateTime GetLatestAvailableTime(Room room1, Room room2, DateTime startDate)
        {
            DateTime time1 = GetLatestAvailableTime(room1, startDate);
            DateTime time2 = GetLatestAvailableTime(room2, startDate);
            if (time1 == startDate)
            {
                return time2;
            }
            else if (time2 == startDate)
            {
                return time1;
            }
            else
            {
                return time1 < time2 ? time1 : time2;
            }
        }

        private static List<Appointment> GetAllNotExpired<T>(List<T> list, DateTime startDate, RoomAppointmentType appointmentType) where T : IHasInterval
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (T temp in list)
            {
                if (startDate < temp.Interval.End)
                {
                    Appointment app = new Appointment(temp.Interval.Start, temp.Interval.End, appointmentType);
                    appointmentList.Add(app);
                }
            }
            return appointmentList;
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
        EquipmentRelocationAppointment,
        RoomMergeAppointment,
        RoomDivergeAppointment,
        Free
    }
}
