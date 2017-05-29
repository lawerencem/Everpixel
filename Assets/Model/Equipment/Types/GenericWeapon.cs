using Model.Abilities;
using Model.Events;
using Model.Slot;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class GenericWeapon : GenericEquipment
    {
        public string Name { get; set; }
        public List<WeaponAbility> Abilities { get; set; }
        public double Accuracy { get; set; }
        public double APReduce { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }
        public double FatigueCost { get; set; }
        public double FatigueReduce { get; set; }
        public double InitiativeReduce { get; set; }
        public double MeleeBlockChance { get; set; }
        public double ParryMod { get; set; }
        public double RangeMod { get; set; }
        public double ShieldDamage { get; set; }
        public WeaponSkillEnum Skill { get; set; }


        public GenericWeapon() : base(SlotEnum.Weapon, EquipmentTypeEnum.Held)
        {
            this.Abilities = new List<WeaponAbility>();
            this.Accuracy = 1;
            this.APReduce = 1;
            this.ArmorIgnore = 1;
            this.ArmorPierce = 1;
            this.BlockIgnore = 1;
            this.Damage = 0;
            this.Durability = 0;
            this.FatigueCost = 1;
            this.FatigueReduce = 0;
            this.InitiativeReduce = 1;
            this.MaxDurability = 0;
            this.ParryMod = 1;
            this.RangeMod = 0;
            this.ShieldDamage = 1;
        }

        public bool IsTypeOfShield()
        {
            if (this.Skill == WeaponSkillEnum.Small_Shield ||
                this.Skill == WeaponSkillEnum.Medium_Shield ||
                this.Skill == WeaponSkillEnum.Large_Shield)
                return true;
            else
                return false;
        }
    }
}
