using Assets.Model.Character.Param;
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

        protected StatModData GetModProto()
        {
            var data = new StatModData();
            data.DurationMod = false;
            data.FlatMod = false;
            return data;
        }
    }
}
