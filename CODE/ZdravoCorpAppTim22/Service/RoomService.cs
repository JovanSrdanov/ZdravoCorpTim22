using Model;
using Repository;
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

            ClearAllReferences(room);
            
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

        #region private

        private void ClearAllReferences(Room room)
        {
            RenovationService.Instance.DeleteByList(new List<Renovation>(room.Renovations));
            EquipmentRelocationService.Instance.DeleteByList(new List<EquipmentRelocation>(room.RelocationDestinations));
            EquipmentRelocationService.Instance.DeleteByList(new List<EquipmentRelocation>(room.RelocationSources));
            RoomMergeService.Instance.DeleteByList(new List<RoomMerge>(room.MergesWhereFirst));
            RoomMergeService.Instance.DeleteByList(new List<RoomMerge>(room.MergesWhereSecond));
            RoomDivergeService.Instance.DeleteByList(new List<RoomDiverge>(room.Diverges));

            ClearRoomEquipment(room);
            ClearRoomMedicalAppointments(room);

            room.RemoveAllRenovations();
            room.RemoveAllRelocationSources();
            room.RemoveAllRelocationDestinations();
            room.RemoveAllMergesWhereFirst();
            room.RemoveAllMergesWhereSecond();
            room.RemoveAllDiverges();
        }

        private void ClearRoomEquipment(Room room)
        {
            List<Equipment> eqToMove = new List<Equipment>(room.Equipment);
            foreach (Equipment eq in eqToMove)
            {
                eq.Room = null;
                EquipmentService.Instance.DeleteByID(eq.Id);
                EquipmentService.Instance.AddWarehouseEquipment(eq);
            }
        }

        private void ClearRoomMedicalAppointments(Room room)
        {
            List<MedicalAppointment> l = new List<MedicalAppointment>(room.MedicalAppointment);
            foreach (MedicalAppointment m in l)
            {
                m.Room = null;
                MedicalAppointmentRepository.Instance.DeleteByID(m.Id);
            }
        }
        #endregion
    }
}