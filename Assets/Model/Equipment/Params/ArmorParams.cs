using System.Collections.Generic;

namespace Model.Equipment
{
    public class ArmorParams
    {
        public ArmorParams() { this.Sprites = new List<int>(); }

        public double APReduce { get; set; }
        public double BlockReduce { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduction { get; set; }
        public string Description { get; set; }
        public double DodgeReduce { get; set; }
        public int Durability { get; set; }
        public double FatigueReduce { get; set; }
        public double InitiativeReduce { get; set; }
        public string Name { get; set; }
        public double ParryReduce { get; set; }
        public List<int> Sprites { get; set; }
        public ArmorSkillEnum Skill { get; set; }
        public double StaminaReduce { get; set; }
        public EquipmentTierEnum Tier { get; set; }
        public ArmorTypeEnum Type { get; set; }
        public int Value { get; set; }
    }
}
