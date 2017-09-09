using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Zone;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class MAbility : AAbility
    {
        public MAbility(EAbility t) : base(t) { }


        public MAbility Copy()
        {
            var ability = new MAbility(this._type);
            ability.SetData(this.Data.Copy());
            return ability;
        }

        public void Display()
        {

        }

        public virtual List<TileController> GetAoETiles(AbilityArgs arg)
        {
            return this._logic.GetAoETiles(arg, (int)this.Data.AoE);
        }

        public double GetAPCost()
        {
            return this.Data.APCost;
        }

        public List<TileController> GetTargetedTiles(AbilityArgs arg)
        {
            var list = new List<TileController>();
            if (this.isSelfCast())
                list.Add(arg.Source.Tile);
            else if (this.isRayCast())
                list.AddRange(this._logic.GetRaycastTiles(arg));
            else
                list.AddRange(this._logic.GetAoETiles(arg, arg.AoE));
            return list;
        }

        public List<TileController> GetTargetableTiles(AbilityArgs arg)
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
            if (this.Data.CastType == ECastType.Raycast)
                return true;
            else
                return false;
        }

        public bool isSelfCast()
        {
            if (this.Data.Range == 0)
                return true;
            else
                return false;
        }

        public virtual bool IsValidActionEvent(AbilityArgs arg) { return false; }
        public virtual bool IsValidEmptyTile(AbilityArgs arg) { return this._logic.IsValidEmptyTile(arg); }
        public virtual bool IsValidEnemyTarget(AbilityArgs arg) { return this._logic.IsValidEnemyTarget(arg); }

        public virtual void Predict(Hit hit)
        {
            
        }

        public virtual void Process(Hit hit)
        {
            
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

        public List<Hit> GetHits(AbilityArgs arg)
        {
            var tiles = this.GetTargetedTiles(arg);
            var hits = new List<Hit>();
            if (this._data.HitsTiles)
            {
                foreach (var tile in tiles)
                {
                    var data = new HitData();
                    var hit = new Hit(data);
                    this.PopulateHitData(hit, tile, arg);
                    hits.Add(hit);
                }
            }
            else
            {
                foreach(var tile in tiles)
                {
                    if (tile.Current != null && tile.Current.GetType().Equals(typeof(CharController)))
                    {
                        var target = tile.Current as CharController;
                        var data = new HitData();
                        var hit = new Hit(data);
                        this.PopulateHitData(hit, target.Tile, arg);
                        hits.Add(hit);
                    }
                }
            }
            return hits;
        }

        protected List<TileController> GetRaycastTiles(AbilityArgs arg)
        {
            return this._logic.GetRaycastTiles(arg);
        }

        protected ZoneArgsCont GetZoneArgs(AbilityArgs arg, TileController tile)
        {
            //var zoneArgs = new ZoneArgsCont();
            //zoneArgs.Caster = arg.Source;
            //zoneArgs.Dur = (int)this.Params.Duration; // TODO;
            //zoneArgs.Handle = arg.Source.Handle;
            //zoneArgs.Tile = tile;
            //return zoneArgs;
            return null;
        }

        protected virtual void PredictBullet(Hit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.PredictBullet(hit);
        }

        protected virtual void PredictMelee(Hit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.PredictMelee(hit);
        }

        protected void ProcessHitBullet(Hit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.ProcessBullet(hit);
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
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.ProcessMelee(hit);
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

        private void PopulateHitData(Hit hit, TileController tile, AbilityArgs args)
        {
            hit.Data.Ability = this;
            hit.Data.IsHeal = this.Data.IsHeal;
            hit.Data.Source = args.Source;
            hit.Data.Target = tile;
        }

        private void ProcessEffects(Hit hit)
        {
            foreach(var effect in this._data.Effects)
            {
                // TODO:
            }
        }

        private void ProcessPerks(Hit hit)
        {
            foreach (var perk in hit.Data.Source.Proxy.GetPerks().GetAbilityModPerks())
                perk.TryModAbility(hit);
        }
    }
}
