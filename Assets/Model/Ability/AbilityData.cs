using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Action;
using Assets.Model.Effect;
using Assets.Model.Equipment.Weapon;
using Assets.Model.Injury;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class AbilityData
    {
        public double AccMod { get; set; }
        public double AoE { get; set; }
        public int APCost { get; set; }
        public double ArmorIgnoreMod { get; set; }
        public double ArmorPierceMod { get; set; }
        public double BlockIgnoreMod { get; set; }
        public ECastType CastType { get; set; }
        public bool CustomCastCamera { get; set; }
        public bool CustomGraphics { get; set; }
        public double DamageMod { get; set; }
        public double DmgPerPower { get; set; }
        public double DodgeMod { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public List<MEffect> Effects { get; set; }
        public double FlatDamage { get; set; }
        public bool HitsTiles { get; set; }
        public bool Hostile { get; set; }
        public int IconSprite { get; set; }
        public List<EInjury> Injuries { get; set; }
        public bool IsHeal { get; set; }
        public EMagicType MagicType { get; set; }
        public double MeleeBlockChanceMod { get; set; }
        public MWeapon ParentWeapon { get; set; }
        public double ParryModMod { get; set; }
        public MAction ParentAction { get; set; }
        public int Range { get; set; }
        public double RangeBlockMod { get; set; }
        public double RechargeTime { get; set; }
        public EResistType Resist { get; set; }
        public double ShieldDamageMod { get; set; }
        public int SpellLevel { get; set; }
        public int StaminaCost { get; set; }

        public AbilityData()
        {
            this.AccMod = 1;
            this.AoE = 0;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CustomCastCamera = false;
            this.Effects = new List<MEffect>();
            this.DamageMod = 1;
            this.DmgPerPower = 0.05;
            this.DodgeMod = 1;
            this.Duration = 0;
            this.HitsTiles = false;
            this.Hostile = true;
            this.IconSprite = 0;
            this.Injuries = new List<EInjury>();
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.Range = 0;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.StaminaCost = 0;
        }

        public AbilityData Copy()
        {
            var data = new AbilityData();
            data.SpellLevel = this.SpellLevel;
            data.AccMod = this.AccMod;
            data.APCost = this.APCost;
            data.AoE = this.AoE;
            data.ArmorIgnoreMod = this.ArmorIgnoreMod;
            data.ArmorPierceMod = this.ArmorPierceMod;
            data.BlockIgnoreMod = this.BlockIgnoreMod;
            data.CastType = this.CastType;
            data.DamageMod = this.DamageMod;
            data.Description = this.Description;
            data.DodgeMod = this.DodgeMod;
            data.Duration = this.Duration;
            data.Effects = this.Effects;
            data.HitsTiles = this.HitsTiles;
            data.Hostile = this.Hostile;
            data.IconSprite = this.IconSprite;
            data.IsHeal = this.IsHeal;
            data.MagicType = this.MagicType;
            data.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            data.ParryModMod = this.ParryModMod;
            data.ParentWeapon = this.ParentWeapon;
            data.RangeBlockMod = this.RangeBlockMod;
            data.RechargeTime = this.RechargeTime;
            data.ShieldDamageMod = this.ShieldDamageMod;
            data.SpellLevel = this.SpellLevel;
            data.StaminaCost = this.StaminaCost;
            return data;
        }
    }
}
