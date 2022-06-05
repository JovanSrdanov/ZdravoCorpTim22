using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class RoomDivergeService : GenericService<RoomDivergeRepository, RoomDiverge>
    {
        private RoomDivergeService() : base(RoomDivergeRepository.Instance) { }
        public static RoomDivergeService Instance
        {
            get
            {
                return new RoomDivergeService();
            }
        }
        public void DeleteByList(List<RoomDiverge> list)
        {
            foreach (RoomDiverge roomDiverge in list)
            {
                DeleteByID(roomDiverge.Id);
            }
        }

        //Method that's being run every second from the background thread
        public void BackgroundTask()
        {
            List<RoomDiverge> list = GetAllExpired();
            if(list.Count > 0)
            {
                App.Current?.Dispatcher?.Invoke(DoExpiredDiverges);
            }
        }

        public void DivergeInstant(Room room_1, Room room_2, Room sourceRoom, List<Equipment> equipment_1, List<Equipment> equipment_2)
        {
            if (room_1.CanMergeOrDiverge() && room_2.CanMergeOrDiverge())
            {
                Diverge(room_1, room_2, sourceRoom, equipment_1, equipment_2);
            }
        }

        public void Create(Room room_1, Room room_2, Room sourceRoom, List<Equipment> equipment_1, List<Equipment> equipment_2, Interval interval)
        {
            if (room_1.CanMergeOrDiverge() && room_2.CanMergeOrDiverge())
            {
                RoomDiverge diverge = new RoomDiverge
                {
                    FirstRoom = room_1,
                    SecondRoom = room_2,
                    SourceRoom = sourceRoom,
                    FirstRoomEquipment = equipment_1,
                    SecondRoomEquipment = equipment_2,
                    Interval = interval
                };
                Create(diverge);
            }
        }

        #region private

        private void DoExpiredDiverges()
        {
            List<RoomDiverge> list = GetAllExpired();
            foreach (RoomDiverge item in list)
            {
                Room room_1 = item.FirstRoom;
                Room room_2 = item.SecondRoom;
                Room sourceRoom = item.SourceRoom;
                item.SourceRoom = null;
                List<Equipment> equipment_1 = item.FirstRoomEquipment;
                List<Equipment> equipment_2 = item.SecondRoomEquipment;
                Diverge(room_1, room_2, sourceRoom, equipment_1, equipment_2);
                DeleteByID(item.Id);
            }
        }

        private void Diverge(Room room_1, Room room_2, Room sourceRoom, List<Equipment> equipment_1, List<Equipment> equipment_2)
        {
            if (room_1 != null && room_2 != null && sourceRoom != null && equipment_1 != null && equipment_2 != null)
            {
                RoomService.Instance.Create(room_1);
                RoomService.Instance.Create(room_2);

                room_1.Equipment = equipment_1;
                room_2.Equipment = equipment_2;

                foreach (Equipment eq in equipment_1)
                {
                    EquipmentService.Instance.Create(eq);
                }
                foreach (Equipment eq in equipment_2)
                {
                    EquipmentService.Instance.Create(eq);
                }
                RoomService.Instance.Update(room_1);
                RoomService.Instance.Update(room_2);

                foreach (Equipment eq in sourceRoom.Equipment)
                {
                    EquipmentService.Instance.DeleteByID(eq.Id);
                }
                sourceRoom.RemoveAllEquipment();
                RoomService.Instance.DeleteByID(sourceRoom.Id);
            }
        }

        private List<RoomDiverge> GetAllExpired()
        {
            List<RoomDiverge> list = new List<RoomDiverge>();
            lock (RoomMergeRepository.Instance._lock)
            {
                foreach (RoomDiverge item in GetAll())
                {
                    if (item.Interval.End <= DateTime.Now)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
