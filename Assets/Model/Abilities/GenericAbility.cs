using Controller.Managers;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Injuries;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class GenericAbility
    {
        protected bool _hostile = true;
        protected object _type;
        protected string _typeStr = "";

        public bool Hostile { get { return this._hostile; } }
        public object Type { get { return this._type; } }
        public string TypeStr { get { return this._typeStr; } }
        
        public GenericAbilityModData ModData { get; set; }
        public List<InjuryEnum> Injuries { get; set; }
        public int StaminaCost { get; set; }

        public double AccMod { get; set; }
        public int APCost { get; set; }
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

        public virtual void ProcessAbility(HitInfo hit) { }

        public double GetAPCost() { return this.APCost; }

        public GenericAbility()
        {
            this.Injuries = new List<InjuryEnum>();
            this.ModData = new GenericAbilityModData();
        }

        public virtual void ProcessBullet(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit.Ability);
            CombatReferee.Instance.ProcessBullet(hit);
            this.TryApplyInjury(hit);
        }

        public virtual void ProcessMelee(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit.Ability);
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
                if (currentHP > 0)
                {
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
}
