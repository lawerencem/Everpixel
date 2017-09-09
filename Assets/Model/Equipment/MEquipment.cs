using Assets.Model.Equipment.Enum;

namespace Assets.Model.Equipment
{
    public class MEquipmentData
    {
        public int Durability { get; set; }
        public int MaxDurability { get; set; }
        public EEquipmentTier Tier { get; set; }
        public double Value { get; set; }
    }

    public class MEquipment : AEquipment 
    {
        public MEquipment(EEquipmentType t) { this.Type = t; }
    }
}
