using Characters.Params;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class GenericWeapon : GenericEquipment
    {
        public string Name { get; set; }
        public List<GenericAbility> Abilities { get; set; }
        public double Accuracy { get; set; }
        public double APReduce { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }
        public double FatigueCostMod { get; set; }
        public double InitiativeReduce { get; set; }
        public double MeleeBlockChance { get; set; }
        public double ParryMod { get; set; }
        public double RangeMod { get; set; }
        public double ShieldDamage { get; set; }
        public WeaponSkillEnum Skill { get; set; }
        public double StaminaReduce { get; set; }
        public WeaponTypeEnum WpnType { get; set; }

        public GenericWeapon() : base(EquipmentTypeEnum.Held)
        {
            this.Abilities = new List<GenericAbility>();
            this.Accuracy = 1;
            this.APReduce = 1;
            this.ArmorIgnore = 1;
            this.ArmorPierce = 1;
            this.BlockIgnore = 1;
            this.Damage = 0;
            this.Durability = 0;
            this.FatigueCostMod = 1;
            this.InitiativeReduce = 1;
            this.MaxDurability = 0;
            this.ParryMod = 1;
            this.RangeMod = 0;
            this.ShieldDamage = 1;
            this.StaminaReduce = 1;
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

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.AP, this.APReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Initiative, this.InitiativeReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Parry, this.ParryMod));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Stamina, this.StaminaReduce));

            return toReturn;
        }
    }
}
