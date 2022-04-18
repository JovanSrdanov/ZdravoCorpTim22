using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RenovationViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; set; }

        public RenovationViewModel(Room room, DateTime date)
        {
            Appointments = new ObservableCollection<Appointment>(CreateFullList(room, date));
        }

        public RenovationViewModel(Room room, DateTime date, DateTime startDate)
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

        public List<Appointment> GetAppointmentsForDay(Room room, DateTime date)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (MedicalAppointment medApp in room.MedicalAppointment)
            {
                if (date.ToShortDateString().Equals(medApp.MedicalAppointmentStartDateTime.ToShortDateString()))
                {
                    Interval interval = new Interval()
                    {
                        Start = medApp.MedicalAppointmentStartDateTime,
                        End = medApp.MedicalAppointmentEndDateTime
                    };
                    Appointment app = new Appointment(interval, RoomAppointmentType.DoctorAppointment);
                    appointmentList.Add(app);
                }
            }

            appointmentList.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));
            return appointmentList;
        }
        public List<Appointment> CreateFullList(Room room, DateTime date)
        {
            List<Appointment> appointmentList = GetAppointmentsForDay(room, date);

            if (appointmentList.Count == 0)
            {
                Interval interval = new Interval()
                {
                    Start = date.Date,
                    End = date.Date.AddHours(23).AddMinutes(59)
                };
                return new List<Appointment>()
                {
                    new Appointment()
                    {
                        Type = RoomAppointmentType.Free,
                        Interval = interval,
                        CanSpill = true
                    }
                };
            }

            List<Appointment> freeList = new List<Appointment>();
            for (int i = 0; i < appointmentList.Count - 1; i++)
            {
                if (DateTime.Compare(appointmentList[i].Interval.End, appointmentList[i + 1].Interval.Start) < 0)
                {
                    Interval interval = new Interval()
                    {
                        Start = appointmentList[i].Interval.End,
                        End = appointmentList[i + 1].Interval.Start
                    };
                    Appointment app = new Appointment(interval, RoomAppointmentType.Free);
                    freeList.Add(app);
                }
            }

            List<Appointment> allAppointments = new List<Appointment>(appointmentList.Count + freeList.Count + 2);
            allAppointments.AddRange(appointmentList);
            allAppointments.AddRange(freeList);

            allAppointments.Sort((x, y) => DateTime.Compare(x.Interval.Start, y.Interval.Start));

            if (allAppointments.Count > 0)
            {
                DateTime first = allAppointments[0].Interval.Start;
                if (DateTime.Compare(first, first.Date) > 0)
                {
                    Interval interval = new Interval()
                    {
                        Start = first.Date,
                        End = first
                    };
                    Appointment app = new Appointment(interval, RoomAppointmentType.Free);
                    allAppointments.Insert(0, app);
                }
                DateTime last = allAppointments[allAppointments.Count - 1].Interval.End;
                if (DateTime.Compare(last, last.Date.AddHours(23).AddMinutes(59)) < 0)
                {
                    Interval interval = new Interval()
                    {
                        Start = last,
                        End = last.Date.AddHours(23).AddMinutes(59)
                    };
                    Appointment app = new Appointment(interval, RoomAppointmentType.Free, true);
                    allAppointments.Insert(allAppointments.Count, app);
                }
            }
            return allAppointments;
        }
        public static DateTime GetLatestAvailableTime(Room room, DateTime startDate)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            foreach (MedicalAppointment medApp in room.MedicalAppointment)
            {
                if (DateTime.Compare(startDate, medApp.MedicalAppointmentEndDateTime) < 1)
                {
                    Interval interval = new Interval()
                    {
                        Start = medApp.MedicalAppointmentStartDateTime,
                        End = medApp.MedicalAppointmentEndDateTime
                    };
                    Appointment app = new Appointment(interval, RoomAppointmentType.DoctorAppointment);
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
    
    public struct Appointment
    {
        public Interval Interval { get; set; }
        public RoomAppointmentType Type { get; set; }
        public bool CanSpill { get; set; }
        public Appointment(Interval interval, RoomAppointmentType type){
            Interval = interval;
            Type = type;
            CanSpill = false;
        }
        public Appointment(Interval interval, RoomAppointmentType type, bool canSpill)
        {
            Interval = interval;
            Type = type;
            CanSpill = canSpill;
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
