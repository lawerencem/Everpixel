using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Utilities;
using Model.Abilities.Magic;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Injuries;
using Model.Map;
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
            this.AoE = 0;
            this.APCost = 1;
            this.ArmorIgnoreMod = 1;
            this.ArmorPierceMod = 1;
            this.BlockIgnoreMod = 1;
            this.CastTime = 0;
            this.CustomCastCamera = false;
            this.DamageMod = 1;
            this.DmgPerPower = 0.15;
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

        public virtual List<TileController> GetAoETiles(PerformActionEvent e)
        {
            var list = new List<TileController>();
            list.Add(e.ActionContainer.Target);
            return list;
        }

        public virtual List<TileController> GetLOSTiles(PerformActionEvent e)
        {
            var list = new List<TileController>();
            var source = e.ActionContainer.Source.CurrentTile.Model;
            var target = e.ActionContainer.Target.Model;
            HexTile initTile;
            if (source.IsHexN(target, e.ActionContainer.Action.Range))
                initTile = source.GetN();
            else if (source.IsHexNE(target, e.ActionContainer.Action.Range))
                initTile = source.GetNE();
            else if (source.IsHexSE(target, e.ActionContainer.Action.Range))
                initTile = source.GetSE();
            else if (source.IsHexS(target, e.ActionContainer.Action.Range))
                initTile = source.GetS();
            else if (source.IsHexSW(target, e.ActionContainer.Action.Range))
                initTile = source.GetSW();
            else 
                initTile = source.GetNW();
            var hexes = initTile.GetLOSTiles(source, e.ActionContainer.Action.Range);
            foreach (var hex in hexes)
                list.Add(hex.Parent);
            e.ActionContainer.Target = list[list.Count - 1];
            return list;
        }

        public List<TileController> GetTargetTiles(AttackSelectedEvent e, GenericCharacterController c, CombatManager m)
        {
            var list = new List<TileController>();
            if (this.isSelfCast())
                list.Add(c.CurrentTile);
            else if (this.isLoSCast())
                list.AddRange(this.GetAdjacentTiles(c));
            else
                list.AddRange(this.GetStandardAttackTiles(e, c, m));
            return list;
        }

        public bool isSelfCast()
        {
            if (this.Range == 0)
                return true;
            else
                return false;
        }

        public bool isLoSCast()
        {
            if (this.CastType == AbilityCastTypeEnum.LOS_Cast)
                return true;
            else
                return false;
        }

        public virtual void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.OnActionPerks)
                perk.TryProcessAction(e);
        }

        public void ProcessBullet(HitInfo hit)
        {
            if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
            {
                foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                    perk.TryModAbility(hit.Ability);
                CombatReferee.Instance.ProcessBullet(hit);
            }
        }

        public void ProcessLoS(HitInfo hit)
        {
            if (hit.Target != null)
            {
                if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
                {
                    foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                        perk.TryModAbility(hit.Ability);
                    CombatReferee.Instance.ProcessRay(hit);
                }
            }
            else
                hit.Done();
        }

        public void ProcessMelee(HitInfo hit)
        {
            if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
            {
                foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                    perk.TryModAbility(hit.Ability);
                CombatReferee.Instance.ProcessMelee(hit);
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

        public bool IsSingleFX()
        {
            if (this.CastType == AbilityCastTypeEnum.LOS_Cast)
                return true;
            else
                return false;
        }

        public void TryApplyInjury(HitInfo hit)
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

        protected void DetermineRayDirection(HexTile source, HexTile target, int dist)
        {

        }

        protected List<TileController> GetAdjacentTiles(GenericCharacterController c)
        {
            var list = new List<TileController>();
            foreach (var neighbor in c.CurrentTile.Adjacent)
                list.Add(neighbor);
            return list;
        }

        protected List<TileController> GetStandardAttackTiles(
            AttackSelectedEvent e, 
            GenericCharacterController c,
            CombatManager m)
        {
            int distMod = 0;
                distMod += GenericAbilityTable.Instance.Table[e.AttackType].Range;
            
            if (e.RWeapon)
            {
                if (c.Model.RWeapon != null)
                    distMod += (int)c.Model.RWeapon.RangeMod;
            }
            else
            {
                if (c.Model.LWeapon != null)
                    distMod += (int)c.Model.LWeapon.RangeMod;
            }
            var hexTiles = m.Map.GetAoETiles(c.CurrentTile.Model, distMod);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexTiles)
            {
                tileControllers.Add(hex.Parent);
                TileControllerFlags.SetAwaitingActionFlagTrue(hex.Parent.Flags);
            }
            return tileControllers;
        }

        protected bool isValidEmptyTile(PerformActionEvent e)
        {
            if (e.ActionContainer.Target.Model.Current == null)
                return true;
            return false;
        }
        
        protected bool IsValidEnemyTarget(PerformActionEvent e)
        {
            if (e.ActionContainer.Source.LParty)
            {
                if (e.ActionContainer.TargetCharController != null && !e.ActionContainer.TargetCharController.LParty)
                    return true;
            }
            else if (!e.ActionContainer.Source.LParty)
            {
                if (e.ActionContainer.TargetCharController != null && e.ActionContainer.TargetCharController.LParty)
                    return true;
            }
            return false;
        }
    }
}
