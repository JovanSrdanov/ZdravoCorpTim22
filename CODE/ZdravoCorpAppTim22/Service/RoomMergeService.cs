using Model;
using Service;
using System.Collections.Generic;
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
        public void MergeInstant(Room room_1, Room room_2, Room newRoom)
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

        public void Create(Room room_1, Room room_2, Room newRoom, Interval interval)
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
}
