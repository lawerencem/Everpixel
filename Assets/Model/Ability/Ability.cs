using Assets.Controller.Managers;
using Assets.Model.Ability.Enum;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Utilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Injuries;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class Ability : AAbility
    {
        public Ability(EnumAbility type)
        {
            this.Params = new AbilityParamContainer();
        }

        public Ability Copy()
        {
            var ability = new Ability(this._type);
            ability.Params = this.Params.Copy();
            return ability;
        }

        public double GetAPCost()
        {
            return this.Params.APCost;
        }

        public List<TileController> GetTargetTiles(AttackSelectedEvent e, GenericCharacterController c, CombatManager m)
        {
            var list = new List<TileController>();
            if (this.isSelfCast())
                list.Add(c.CurrentTile);
            else if (this.isRayCast())
                list.AddRange(this._logic.GetAdjacentTiles(c));
            else
                list.AddRange(this._logic.GetStandardAttackTiles(e, c, m));
            return list;
        }

        public bool isRayCast()
        {
            if (this.Params.CastType == CastTypeEnum.Raycast)
                return true;
            else
                return false;
        }

        public bool isSelfCast()
        {
            if (this.Params.Range == 0)
                return true;
            else
                return false;
        }

        public virtual bool IsValidActionEvent(PerformActionEvent e)
        {
            return false;
        }

        public virtual void PredictAbility(HitInfo hit)
        {
            hit.ModData.Reset();
            foreach (var perk in hit.Source.Model.Perks.OnActionPerks)
                perk.TryProcessAction(hit);
        }

        public virtual void PredictBullet(HitInfo hit)
        {
            // TODO: Add prehit perk stuff to all prediction
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
            this._logic.PredictBullet(hit);
        }

        public virtual void PredictMelee(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
            AbilityLogic.Instance.PredictMelee(hit);
        }

        public virtual void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            hit.ModData.Reset();
            if (hit.Target != null)
            {
                foreach (var perk in hit.Target.Model.Perks.PreHitPerks)
                    perk.TryModHit(hit);
                foreach (var perk in e.Container.Source.Model.Perks.OnActionPerks)
                    perk.TryProcessAction(hit);
            }
        }

        public void ProcessBullet(HitInfo hit)
        {
            if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
            {
                foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                    perk.TryModAbility(hit);
                AbilityLogic.Instance.ProcessBullet(hit);
            }
        }

        public void ProcessLoS(HitInfo hit)
        {
            if (hit.Target != null)
            {
                if (!CharacterStatusFlags.HasFlag(hit.Target.Model.StatusFlags.CurFlags, CharacterStatusFlags.Flags.Dead))
                {
                    foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                        perk.TryModAbility(hit);
                    AbilityLogic.Instance.ProcessRay(hit);
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
                    perk.TryModAbility(hit);
                AbilityLogic.Instance.ProcessMelee(hit);
            }
        }

        public void ProcessSummon(HitInfo hit)
        {
            AttackEventFlags.SetSummonTrue(hit.Flags);
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
            AbilityLogic.Instance.ProcessSummon(hit);
        }

        public void ProcessShapeshift(HitInfo hit)
        {
            AttackEventFlags.SetShapeshiftTrue(hit.Flags);
            CharacterStatusFlags.SetShapeshiftedTrue(hit.Source.Model.StatusFlags);
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
            AbilityLogic.Instance.ProcessShapeshift(hit);
        }

        public void ProcessSong(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
            AbilityLogic.Instance.ProcessSong(hit);
        }

        public void ProcessZone(HitInfo hit)
        {
            foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
                perk.TryModAbility(hit);
        }

        public void TryApplyInjury(HitInfo hit)
        {
            if (!AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge) &&
                !AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry) &&
                hit.Target != null)
            {
                var roll = RNG.Instance.NextDouble();
                var hp = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                var currentHP = hit.Target.Model.GetCurrentHP();
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