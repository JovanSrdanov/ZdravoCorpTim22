using Model;

namespace ZdravoCorpAppTim22.Model
{
    public class EquipmentRelocation
    {
        public int id;

        public Equipment equipment;
        public Equipment Equipment
        {
            get
            {
                return equipment;
            }
            set
            {
                if (this.equipment == null || !this.equipment.Equals(value))
                {
                    if (this.equipment != null)
                    {
                        Equipment oldEquipment = this.equipment;
                        this.equipment = null;
                        oldEquipment.RemoveRelocationEquipment(this);
                    }
                    if (value != null)
                    {
                        this.equipment = value;
                        this.equipment.AddRelocationEquipment(this);
                    }
                }
            }
        }
    }
}
