using Controller.Managers;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Injuries;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class WeaponAbility : GenericAbility
    {
        public double AccMod { get; set; }
        public double ArmorIgnoreMod { get; set; }
        public double ArmorPierceMod { get; set; }
        public double BlockIgnoreMod { get; set; }
        public double DamageMod { get; set; }
        public double DodgeMod { get; set; }
        public string Description { get; set; }
        public double MeleeBlockChanceMod { get; set; }
        public double ParryModMod { get; set; }
        public int Range { get; set; }
        public double RangeBlockMod { get; set; }
        public double ShieldDamageMod { get; set; }
        private new WeaponAbilitiesEnum _type;
        public new WeaponAbilitiesEnum Type { get { return this._type; } }

        public WeaponAbility(WeaponAbilitiesEnum type)
        {
            this.AccMod = 1;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.DamageMod = 1;
            this.DodgeMod = 1;
            this.MeleeBlockChanceMod = 1;
            this.ParryModMod = 1;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.StaminaCost = 0;
            this._type = type;
            this._typeStr = this._type.ToString().Replace("_", " ");
        }

        public WeaponAbility Copy()
        {
            var ability = new WeaponAbility(this._type);
            ability.AccMod = this.AccMod;
            ability.APCost = this.APCost;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.DamageMod = this.DamageMod;
            ability.Description = this.Description;
            ability.DodgeMod = this.DodgeMod;
            ability.StaminaCost = this.StaminaCost;
            ability.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            ability.ParryModMod = this.ParryModMod;
            ability.RangeBlockMod = this.RangeBlockMod;
            ability.ShieldDamageMod = this.ShieldDamageMod;
            return ability;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            
        }

        public virtual void ProcessMelee(HitInfo hit)
        {
            CombatReferee.Instance.ProcessMelee(hit);
            this.TryApplyInjury(hit);
        }

        protected virtual void TryApplyInjury(HitInfo hit)
        {
            if (!AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge) &&
                !AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
            {
                var roll = RNG.Instance.NextDouble();
                var hp = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                var currentHP = hit.Target.Model.CurrentHP;
                var chance = ((double)hit.Dmg / (double)hp) * (hp / currentHP);
                if (roll < chance)
                {
                    if (this.Injuries.Count > 0)
                    {
                        var injuryType = ListUtil<InjuryEnum>.GetRandomListElement(this.Injuries);
                        var injuryParams = InjuryTable.Instance.Table[injuryType];
                        var injury = injuryParams.GetGenericInjury();
                        var apply = new ApplyInjuryEvent(CombatEventManager.Instance, hit, injury);
                    }
                }
            }
        }
    }
}
