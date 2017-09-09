using Assets.Model.Ability.Enum;
using Assets.Model.Effect;
using Assets.Model.Equipment.Enum;
using Assets.View.Fatality;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Param
{
    public class WeaponParams
    {
        public List<EAbility> Abilities { get; set;}
        public double AccuracyMod { get; set; }
        public double APMod { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public bool CustomBullet { get; set; }
        public EFatality CustomFatality { get; set; }
        public int Damage { get; set; }
        public double Dodge_Mod { get; set; }
        public string Description { get; set; }
        public int MaxDurability { get; set; }
        public List<EEffect> Effects { get; set; }
        public double FatigueMod { get; set; }
        public double InitiativeMod { get; set; }
        public double MeleeBlockChance { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public int RangeMod { get; set; }
        public double RangeBlockChance { get; set; }
        public double ShieldDamagePercent { get; set; }
        public EWeaponSkill Skill { get; set; }
        public List<int> Sprites { get; set; }
        public string SpriteFXPath { get; set; }
        public double StaminaMod { get; set; }
        public EEquipmentTier Tier { get; set; }
        public EWeaponType Type { get; set; }
        public EWeaponUse Use { get; set; }
        public double Value { get; set; }

        public WeaponParams()
        {
            this.Abilities = new List<EAbility>();
            this.Effects = new List<EEffect>();
        }
    }
}
