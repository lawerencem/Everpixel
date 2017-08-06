using Assets.Model.Equipment.Enum;

namespace Assets.Model.Equipment.Type
{
    abstract public class AEquipment
    {
        public int Durability { get; set; }
        public int MaxDurability { get; set; }
        public EEquipmentTier Tier { get; set; }
        public EEquipmentType Type { get; set; }
    }
}
