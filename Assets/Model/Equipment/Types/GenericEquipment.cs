using Model.Slot;

namespace Model.Equipment
{
    public class GenericEquipment : AbstractEquipment 
    {
        public GenericEquipment(SlotEnum s, EquipmentTypeEnum t) { this.Slot = s; this.Type = t; }
    }
}
