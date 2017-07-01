using Controller.Managers;
using Controller.Map;
using Generics.Utilities;
using Model.Abilities.Magic;
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
        protected AbilitiesEnum _type;

        public bool Hostile { get { return this._hostile; } }
        public AbilitiesEnum Type { get { return this._type; } }
        
        public GenericAbilityModData ModData { get; set; }
        public List<InjuryEnum> Injuries { get; set; }

        public double AccMod { get; set; }
        public double AoE { get; set; }
        public int APCost { get; set; }
        public double ArmorIgnoreMod { get; set; }
        public double ArmorPierceMod { get; set; }
        public double BlockIgnoreMod { get; set; }
        public double CastTime { get; set; }
        public AbilityCastTypeEnum CastType { get; set; }
        public bool CustomCastCamera { get; set; }
        public double DamageMod { get; set; }
        public double DmgPerPower { get; set; }
        public double DodgeMod { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public double FlatDamage { get; set; }
        public MagicTypeEnum MagicType { get; set; }
        public double MeleeBlockChanceMod { get; set; }
        public double ParryModMod { get; set; }
        public int Range { get; set; }
        public double RangeBlockMod { get; set; }
        public double RechargeTime { get; set; }
        public double ShieldDamageMod { get; set; }
        public int SpellLevel { get; set; }
        public int Sprite { get; set; }
        public int StaminaCost { get; set; }

        public GenericAbility(AbilitiesEnum type)
        {
            this.AccMod = 1;
            this.AoE = 1;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CastTime = 0;
            this.CustomCastCamera = false;
            this.DamageMod = 1;
            this.DmgPerPower = 0;
            this.DodgeMod = 1;
            this.Duration = 0;
            this.Injuries = new List<InjuryEnum>();
            this.MeleeBlockChanceMod = 1;
            this.ModData = new GenericAbilityModData();
            this.ParryModMod = 1;
            this.Range = 0;
            this.RangeBlockMod = 1;
            this.ShieldDamageMod = 1;
            this.Sprite = 0;
            this.StaminaCost = 0;
            this._type = type;
        }

        public virtual void ProcessAbility(HitInfo hit)
        {

        }

        public GenericAbility Copy()
        {
            var ability = new GenericAbility(this._type);
            ability.SpellLevel = this.SpellLevel;
            ability.AccMod = this.AccMod;
            ability.APCost = this.APCost;
            ability.AoE = this.AoE;
            ability.ArmorIgnoreMod = this.ArmorIgnoreMod;
            ability.ArmorPierceMod = this.ArmorPierceMod;
            ability.BlockIgnoreMod = this.BlockIgnoreMod;
            ability.CastType = this.CastType;
            ability.DamageMod = this.DamageMod;
            ability.Description = this.Description;
            ability.DodgeMod = this.DodgeMod;
            ability.Duration = this.Duration;
            ability.StaminaCost = this.StaminaCost;
            ability.MagicType = this.MagicType;
            ability.MeleeBlockChanceMod = this.MeleeBlockChanceMod;
            ability.ParryModMod = this.ParryModMod;
            ability.RangeBlockMod = this.RangeBlockMod;
            ability.RechargeTime = this.RechargeTime;
            ability.ShieldDamageMod = this.ShieldDamageMod;
            ability.SpellLevel = this.SpellLevel;
            ability.Sprite = this.Sprite;
            ability.StaminaCost = this.StaminaCost;
            return ability;
        }

        public double GetAPCost() { return this.APCost; }

        public virtual List<TileController> GetAoETiles(TileController target)
        {
            var list = new List<TileController>();
            list.Add(target);
            return list;
        }

        public void ProcessBullet(HitInfo hit)
        {
            if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
            {
                foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                    perk.TryModAbility(hit.Ability);
                CombatReferee.Instance.ProcessBullet(hit);
                this.TryApplyInjury(hit);
            }
        }

        public void ProcessMelee(HitInfo hit)
        {
            if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
            {
                foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                    perk.TryModAbility(hit.Ability);
                CombatReferee.Instance.ProcessMelee(hit);
                this.TryApplyInjury(hit);
            }
        }

        public void ProcessSummon(HitInfo hit)
        {
            AttackEventFlags.SetSummonTrue(hit.Flags);
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit.Ability);
            CombatReferee.Instance.ProcessSummon(hit);
        }

        public void ProcessShapeshift(HitInfo hit)
        {
            AttackEventFlags.SetShapeshiftTrue(hit.Flags);
            CharacterStatusFlags.SetShapeshiftedTrue(hit.Source.Model.StatusFlags);
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit.Ability);
            CombatReferee.Instance.ProcessShapeshift(hit);
        }

        public void ProcessSong(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit.Ability);
            CombatReferee.Instance.ProcessSong(hit);
        }

        public virtual bool IsValidActionEvent(PerformActionEvent e)
        {
            return false;
        }

        protected bool isValidEmptyTile(PerformActionEvent e)
        {
            if (e.Info.Target.Model.Current == null)
                return true;
            return false;
        }
        
        protected bool IsValidEnemyTarget(PerformActionEvent e)
        {
            if (e.Info.Source.LParty)
            {
                if (e.Info.TargetCharController != null && !e.Info.TargetCharController.LParty)
                    return true;
            }
            else if (!e.Info.Source.LParty)
            {
                if (e.Info.TargetCharController != null && e.Info.TargetCharController.LParty)
                    return true;
            }
            return false;
        }

        public bool isSelfCast()
        {
            if (this.Range == 0)
                return true;
            else
                return false;
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
