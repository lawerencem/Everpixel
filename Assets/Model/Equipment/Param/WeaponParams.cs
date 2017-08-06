using Assets.Model.Ability.Enum;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Param
{
    public class WeaponParams
    {
        public WeaponParams() { this.Abilities = new List<EAbility>(); }

        public List<EAbility> Abilities { get; set;}
        public double Accuracy { get; set; }
        public double APReduce { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public int Damage { get; set; }
        public double DodgeMod { get; set; }
        public string Description { get; set; }
        public int Durability { get; set; }
        public double FatigueCostMod { get; set; }
        public double InitiativeReduce { get; set; }
        public double MeleeBlockChance { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public int RangeMod { get; set; }
        public double RangeBlockChance { get; set; }
        public double ShieldDamage { get; set; }
        public EWeaponSkill Skill { get; set; }
        public List<int> Sprites { get; set; }
        public double StaminaReduce { get; set; }
        public EEquipmentTier Tier { get; set; }
        public EWeaponType Type { get; set; }
        public EWeaponUse Use { get; set; }
        public double Value { get; set; }
    }
}
