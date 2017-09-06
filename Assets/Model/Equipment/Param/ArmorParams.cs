using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Param
{
    public class ArmorParams
    {
        public ArmorParams() { this.Sprites = new List<int>(); }

        public double APMod { get; set; }
        public double BlockMod { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageMod { get; set; }
        public string Description { get; set; }
        public double DodgeMod { get; set; }
        public int Durability { get; set; }
        public double FatigueCost { get; set; }
        public double InitativeMod { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public List<int> Sprites { get; set; }
        public double StaminaMod { get; set; }
        public EEquipmentTier Tier { get; set; }
        public EArmorType Type { get; set; }
        public double Value { get; set; }
    }
}
