using Assets.Model.Equipment.Enum;

namespace Assets.Model.Equipment
{
    abstract public class AEquipment
    {
        public EEquipmentTier Tier { get; set; }
        public EEquipmentType Type { get; set; }
    }
}
