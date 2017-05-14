using Assets.Model;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class WeaponParams
    {
        public WeaponParams() { this.Abilities = new List<WeaponAbilitiesEnum>(); }

        public List<WeaponAbilitiesEnum> Abilities { get; set;}
        public double Accuracy { get; set; }
        public double APReduce { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public int Damage { get; set; }
        public double DodgeMod { get; set; }
        public string Description { get; set; }
        public int Durability { get; set; }
        public double FatigueCost { get; set; }
        public double InitiativeReduce { get; set; }
        public double MeleeBlockChance { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public int Range { get; set; }
        public double RangeBlockChance { get; set; }
        public double ShieldDamage { get; set; }
        public WeaponSkillEnum Skill { get; set; }
        public List<int> Sprites { get; set; }
        public double StaminaReduce { get; set; }
        public EquipmentTierEnum Tier { get; set; }
        public WeaponTypeEnum Type { get; set; }
        public WeaponUseEnum Use { get; set; }
        public int Value { get; set; }
    }
}
