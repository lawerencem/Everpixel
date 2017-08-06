using Assets.Model.Ability;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Type
{
    public class MWeapon : MEquipment
    {
        public string Name { get; set; }
        public List<MAbility> Abilities { get; set; }
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
        public EWeaponSkill Skill { get; set; }
        public double StaminaReduce { get; set; }
        public EWeaponType WpnType { get; set; }

        public MWeapon() : base(EEquipmentType.Held)
        {
            this.Abilities = new List<MAbility>();
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
            if (this.Skill == EWeaponSkill.Small_Shield ||
                this.Skill == EWeaponSkill.Medium_Shield ||
                this.Skill == EWeaponSkill.Large_Shield)
                return true;
            else
                return false;
        }

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.AP, this.APReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Initiative, this.InitiativeReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Parry, this.ParryMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Stamina, this.StaminaReduce));

            return toReturn;
        }
    }
}
