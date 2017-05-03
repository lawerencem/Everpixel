using Model.Abilities;
using Model.Equipment;
using System.Collections.Generic;
using Model.Material;

namespace Equipment.Params
{
    public class ConcreteWeaponStats
    {
        EquipmentTierEnum Tier { get; set; }
        public List<WeaponAbilitiesEnum> Abilities { get; set; }
        public double AP { get; set; }
        public double ArmorIgnore { get; set; }
        public double BlockIgnore { get; set; }
        public double CurrentDurability { get; set; }
        public double Damage { get; set; }
        public double Fatigue { get; set; }
        public double InitiativeReduce { get; set; }
        public GenericMaterial Material { get; set; }
        public double MaxDurability { get; set; }
        public double ParryReduce { get; set; }
        public int Range { get; set; }
        public double ShieldDamage { get; set; }
        public WeaponSkillEnum Skill { get; set; }
        public int SpriteIndex { get; set; }
        public double StaminaReduce { get; set; }
        public WeaponUseEnum Use { get; set; }
    }
}
