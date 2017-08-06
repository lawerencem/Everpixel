using Assets.Model.Abilities.Magic;
using Assets.Model.Ability.Enum;
using Assets.Model.Injuries;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class AbilityParamContainer
    {
        public double AccMod { get; set; }
        public double AoE { get; set; }
        public int APCost { get; set; }
        public double ArmorIgnoreMod { get; set; }
        public double ArmorPierceMod { get; set; }
        public double BlockIgnoreMod { get; set; }
        public double CastTime { get; set; }
        public ECastType CastType { get; set; }
        public bool CustomCastCamera { get; set; }
        public double DamageMod { get; set; }
        public double DmgPerPower { get; set; }
        public double DodgeMod { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public double EffectDur { get; set; }
        public double EffectValue { get; set; }
        public double FlatDamage { get; set; }
        public List<EInjury> Injuries { get; set; }
        public MagicTypeEnum MagicType { get; set; }
        public double MeleeBlockChanceMod { get; set; }
        public double ParryModMod { get; set; }
        public int Range { get; set; }
        public double RangeBlockMod { get; set; }
        public double RechargeTime { get; set; }
        public EResistType Resist { get; set; }
        public double ShieldDamageMod { get; set; }
        public int SpellLevel { get; set; }
        public int Sprite { get; set; }
        public int StaminaCost { get; set; }

        public AbilityParamContainer()
        {
            this.AccMod = 1;
            this.AoE = 0;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CastTime = 0;
            this.CustomCastCamera = false;
            this.DamageMod = 1;
            this.DmgPerPower = 0.05;
            this.DodgeMod = 1;
            this.Duration = 0;
            this.Injuries = new List<EInjury>();
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.Range = 0;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.Sprite = 0;
            this.StaminaCost = 0;
        }

        public AbilityParamContainer Copy()
        {
            var aParams = new AbilityParamContainer();
            aParams.SpellLevel = this.SpellLevel;
            aParams.AccMod = this.AccMod;
            aParams.APCost = this.APCost;
            aParams.AoE = this.AoE;
            aParams.ArmorIgnoreMod = this.ArmorIgnoreMod;
            aParams.ArmorPierceMod = this.ArmorPierceMod;
            aParams.BlockIgnoreMod = this.BlockIgnoreMod;
            aParams.CastType = this.CastType;
            aParams.DamageMod = this.DamageMod;
            aParams.Description = this.Description;
            aParams.DodgeMod = this.DodgeMod;
            aParams.Duration = this.Duration;
            aParams.StaminaCost = this.StaminaCost;
            aParams.MagicType = this.MagicType;
            aParams.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            aParams.ParryModMod = this.ParryModMod;
            aParams.RangeBlockMod = this.RangeBlockMod;
            aParams.RechargeTime = this.RechargeTime;
            aParams.ShieldDamageMod = this.ShieldDamageMod;
            aParams.SpellLevel = this.SpellLevel;
            aParams.Sprite = this.Sprite;
            aParams.StaminaCost = this.StaminaCost;
            return aParams;
        }
    }
}
