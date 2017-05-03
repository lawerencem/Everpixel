using Model.Material;
using Model.Slot;

namespace Model.Equipment
{
    abstract public class AbstractEquipment
    {
        public int Durability { get; set; }
        public GenericMaterial Material { get; set; }
        public int MaxDurability { get; set; }
        public SlotEnum Slot { get; set; }
        public EquipmentTierEnum Tier { get; set; }
        public EquipmentTypeEnum Type { get; set; }
    }
}
