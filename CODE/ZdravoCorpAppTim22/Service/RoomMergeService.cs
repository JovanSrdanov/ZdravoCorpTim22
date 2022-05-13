using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class RoomMergeService : GenericService<RoomMergeRepository, RoomMerge>
    {
        private static RoomMergeService instance;
        private RoomMergeService() : base(RoomMergeRepository.Instance) { }
        public static RoomMergeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomMergeService();
                }

                return instance;
            }
        }
        public void BackgroundTask()
        {
            DoExpiredMerges();
        }

        public void DoExpiredMerges()
        {
            List<RoomMerge> list = GetAllExpired();
            if (App.Current != null)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (RoomMerge item in list)
                    {
                        Room room_1 = item.FirstRoom;
                        Room room_2 = item.SecondRoom;
                        Room newRoom = item.NewRoom;
                        item.FirstRoom = null;
                        item.SecondRoom = null;
                        Merge(room_1, room_2, newRoom);
                        DeleteByID(item.Id);
                    }
                });
            }
        }

        public void Merge(Room room_1, Room room_2, Room newRoom)
        {
            if (room_1 != null && room_2 != null && newRoom != null)
            {
                RoomService.Instance.Create(newRoom);
                List<Equipment> temp = new List<Equipment>();
                foreach (Equipment eq in room_1.Equipment)
                {
                    temp.Add(eq);
                    EquipmentService.Instance.AddRoomEquipment(newRoom, eq);
                }
                foreach (Equipment eq in room_2.Equipment)
                {
                    temp.Add(eq);
                    EquipmentService.Instance.AddRoomEquipment(newRoom, eq);
                }
                room_1.RemoveAllEquipment();
                room_2.RemoveAllEquipment();
                foreach (Equipment eq in temp)
                {
                    EquipmentService.Instance.DeleteByID(eq.Id);
                }
                RoomService.Instance.DeleteByID(room_1.Id);
                RoomService.Instance.DeleteByID(room_2.Id);
                RoomService.Instance.Update(newRoom);
            }
        }

        public void MergeInstant(Room room_1, Room room_2, Room newRoom)
        {
            if (room_1.CanMergeOrDiverge() && room_2.CanMergeOrDiverge())
            {
                Merge(room_1, room_2, newRoom);
            }
        }
        public void Create(Room room_1, Room room_2, Room newRoom, Interval interval)
        {
            if (room_1.CanMergeOrDiverge() && room_2.CanMergeOrDiverge())
            {
                RoomMerge merge = new RoomMerge
                {
                    Interval = interval,
                    FirstRoom = room_1,
                    SecondRoom = room_2,
                    NewRoom = newRoom
                };
                Create(merge);
            }
        }
        private List<RoomMerge> GetAllExpired()
        {
            List<RoomMerge> list = new List<RoomMerge>();
            lock (RoomMergeRepository.Instance._lock)
            {
                foreach (RoomMerge item in GetAll())
                {
                    if (item.Interval.End <= DateTime.Now)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}
