using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Param
{
    public class ArmorParams
    {
        public ArmorParams() { this.Sprites = new List<int>(); }

        public double APReduce { get; set; }
        public double BlockReduce { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduction { get; set; }
        public string Description { get; set; }
        public double DodgeMod { get; set; }
        public int Durability { get; set; }
        public double FatigueCost { get; set; }
        public double InitiativeReduce { get; set; }
        public string Name { get; set; }
        public double ParryReduce { get; set; }
        public List<int> Sprites { get; set; }
        public double StaminaReduce { get; set; }
        public EEquipmentTier Tier { get; set; }
        public EArmorType Type { get; set; }
        public double Value { get; set; }
    }
}
