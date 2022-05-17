using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
   public class RoomService : GenericService<RoomRepository, Room>
    {
        private static RoomService instance;
        private RoomService() : base(RoomRepository.Instance) { }
        public static RoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomService();
                }
                return instance;
            }
        }

        public override void DeleteByID(int id)
        {
            Room room = GetByID(id);
            if (room == null) return;

            RenovationService.Instance.DeleteMany(new List<Renovation>(room.Renovations));
            EquipmentRelocationService.Instance.DeleteMany(new List<EquipmentRelocation>(room.RelocationDestinations));
            EquipmentRelocationService.Instance.DeleteMany(new List<EquipmentRelocation>(room.RelocationSources));
            RoomMergeService.Instance.DeleteMany(new List<RoomMerge>(room.MergesWhereFirst));
            RoomMergeService.Instance.DeleteMany(new List<RoomMerge>(room.MergesWhereSecond));
            RoomDivergeService.Instance.DeleteMany(new List<RoomDiverge>(room.Diverges));

            List<Equipment> eqToMove = new List<Equipment>(room.Equipment);
            foreach (Equipment eq in eqToMove)
            {
                eq.Room = null;
                EquipmentService.Instance.DeleteByID(eq.Id);
                EquipmentService.Instance.AddWarehouseEquipment(eq);
            }

            room.RemoveAllRenovations();
            room.RemoveAllRelocationSources();
            room.RemoveAllRelocationDestinations();
            room.RemoveAllMergesWhereFirst();
            room.RemoveAllMergesWhereSecond();
            room.RemoveAllDiverges();

            List<MedicalAppointment> l = new List<MedicalAppointment>(room.MedicalAppointment);
            foreach (MedicalAppointment m in l)
            {
                m.Room = null;
                MedicalAppointmentRepository.Instance.DeleteByID(m.Id);
            }
            base.DeleteByID(id);
        }

        public Room GetByName(string name)
        {
            foreach (Room item in GetAll())
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
    }
}