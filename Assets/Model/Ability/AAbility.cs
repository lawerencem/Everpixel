using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic;
using Assets.Model.Combat;
using Assets.Model.Zone;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public abstract class AAbility
    {
        protected AbilityLogic _logic;
        protected bool _hostile = true;
        protected EAbility _type;

        public bool Hostile { get { return this._hostile; } }
        public EAbility Type { get { return this._type; } }

        public AbilityParamContainer Params { get; set; }

        public MAbility Copy()
        {
            var ability = new MAbility(this._type);
            ability.Params = this.Params.Copy();
            return ability;
        }

        public void Display()
        {

        }

        public virtual List<TileController> GetAoETiles(AbilityArgContainer arg)
        {
            return this._logic.GetAoETiles(arg, (int)this.Params.AoE);
        }

        public double GetAPCost()
        {
            return this.Params.APCost;
        }

        public List<TileController> GetTargetTiles(AbilityArgContainer arg)
        {
            var list = new List<TileController>();
            if (this.isSelfCast())
                list.Add(arg.Source.Tile);
            else if (this.isRayCast())
                list.AddRange(this._logic.GetRaycastTiles(arg));
            else
                list.AddRange(this._logic.GetPotentialTargets(arg));
            return list;
        }

        public bool isRayCast()
        {
            if (this.Params.CastType == ECastType.Raycast)
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

        public virtual bool IsValidActionEvent(AbilityArgContainer arg) { return false; }
        public virtual bool IsValidEmptyTile(AbilityArgContainer arg) { return this._logic.IsValidEmptyTile(arg); }
        public virtual bool IsValidEnemyTarget(AbilityArgContainer arg) { return this._logic.IsValidEnemyTarget(arg); }

        public virtual List<Hit> Predict(AbilityArgContainer arg)
        {
            var hits = this.GetHits(arg);
            //foreach (var perk in hit.Source.Model.Perks.OnActionPerks)
            //    perk.TryProcessAction(hit);
            return hits;
        }

        public virtual List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = this.GetHits(arg);

            return hits;
        }

        public void TryApplyInjury(Hit hit)
        {
            //if (!FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Dodge) &&
            //    !FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Parry) &&
            //    hit.Target != null)
            //{
            //    var roll = RNG.Instance.NextDouble();
            //    var hp = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP);
            //    var currentHP = hit.Target.Model.GetCurrentHP();
            //    if (currentHP > 0)
            //    {
            //        var chance = ((double)hit.Dmg / (double)hp) * (hp / currentHP);
            //        if (roll < chance)
            //        {
            //            if (this.Params.Injuries.Count > 0)
            //            {
            //                var injuryType = ListUtil<EInjury>.GetRandomListElement(this.Params.Injuries);
            //                var injuryParams = InjuryTable.Instance.Table[injuryType];
            //                var injury = injuryParams.GetInjury();
            //                var apply = new ApplyInjuryEvent(CombatEventManager.Instance, hit, injury);
            //            }
            //        }
            //    }
            //}
        }

        protected List<Hit> GetHits(AbilityArgContainer arg)
        {
            var tiles = this.GetTargetTiles(arg);
            var hits = new List<Hit>();
            foreach (var tile in tiles) { hits.Add(new Hit(arg)); }
            return hits;
        }

        protected List<TileController> GetRaycastTiles(AbilityArgContainer arg)
        {
            return this._logic.GetRaycastTiles(arg);
        }

        protected ZoneArgsCont GetZoneArgs(AbilityArgContainer arg, TileController tile)
        {
            //var zoneArgs = new ZoneArgsCont();
            //zoneArgs.Caster = arg.Source;
            //zoneArgs.Dur = (int)this.Params.Duration; // TODO;
            //zoneArgs.Handle = arg.Source.Handle;
            //zoneArgs.Tile = tile;
            //return zoneArgs;
            return null;
        }

        protected virtual List<Hit> PredictBullet(AbilityArgContainer arg)
        {
            //// TODO: Add prehit perk stuff to all prediction
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
            //this._logic.PredictBullet(hit);
            var hits = this.GetHits(arg);
            return hits;
        }

        protected virtual List<Hit> PredictMelee(AbilityArgContainer arg)
        {
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
            //this._logic.PredictMelee(hit);
            var hits = this.GetHits(arg);
            //foreach (var perk in hit.Source.Model.Perks.OnActionPerks)
            //    perk.TryProcessAction(hit);
            return hits;
        }

        protected void ProcessHitBullet(Hit hit)
        {
            //if (!FCharacterStatus.HasFlag(hit.Target.Model.StatusFlags.CurFlags, FCharacterStatus.Flags.Dead))
            //{
            //    foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //        perk.TryModAbility(hit);
            //    this._logic.ProcessBullet(hit);
            //}
        }

        protected void ProcessHitLoS(Hit hit)
        {
            //if (hit.Target != null)
            //{
            //    if (!FCharacterStatus.HasFlag(hit.Target.Model.StatusFlags.CurFlags, FCharacterStatus.Flags.Dead))
            //    {
            //        foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //            perk.TryModAbility(hit);
            //        this._logic.ProcessRay(hit);
            //    }
            //}
            //else
            //    hit.Done();
        }

        protected void ProcessHitMelee(Hit hit)
        {
            //if (!FCharacterStatus.HasFlag(hit.Target.Model.StatusFlags.CurFlags, FCharacterStatus.Flags.Dead))
            //{
            //    foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //        perk.TryModAbility(hit);
            //    this._logic.ProcessMelee(hit);
            //}
        }

        protected void ProcessHitSummon(Hit hit)
        {
            //FHit.SetSummonTrue(hit.Flags);
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
            //this._logic.ProcessSummon(hit);
        }

        protected void ProcessShapeshift(Hit hit)
        {
            //FHit.SetShapeshiftTrue(hit.Flags);
            //FCharacterStatus.SetShapeshiftedTrue(hit.Source.Model.StatusFlags);
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
            //this._logic.ProcessShapeshift(hit);
        }

        protected void ProcessHitSong(Hit hit)
        {
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
            //this._logic.ProcessSong(hit);
        }

        protected void ProcessHitZone(Hit hit)
        {
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
        }
    }
}
