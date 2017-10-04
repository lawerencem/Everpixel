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

        public virtual List<CTile> GetAoETiles(AbilityArgs arg)
        {
            return this._logic.GetAoETiles(arg, (int)this.Data.AoE);
        }

        public double GetAPCost()
        {
            return this.Data.APCost;
        }

        public List<CTile> GetTargetedTiles(AbilityArgs arg)
        {
            var list = new List<CTile>();
            if (this.isSelfCast())
                list.Add(arg.Source.Tile);
            else if (this.isRayCast())
                list.AddRange(this._logic.GetRaycastTiles(arg));
            else
                list.AddRange(this._logic.GetAoETiles(arg, arg.AoE));
            return list;
        }

        public List<CTile> GetTargetableTiles(AbilityArgs arg)
        {
            var list = new List<CTile>();
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

        public virtual void Predict(MHit hit)
        {
            
        }

        public virtual void Process(MHit hit)
        {
            
        }

        public List<MHit> GetHits(AbilityArgs arg)
        {
            var tiles = this.GetTargetedTiles(arg);
            var hits = new List<MHit>();
            if (this._data.HitsTiles)
            {
                foreach (var tile in tiles)
                {
                    var data = new HitData();
                    var hit = new MHit(data);
                    this.PopulateHitData(hit, tile, arg);
                    hits.Add(hit);
                }
            }
            else
            {
                foreach(var tile in tiles)
                {
                    if (tile.Current != null && tile.Current.GetType().Equals(typeof(CChar)))
                    {
                        var target = tile.Current as CChar;
                        var data = new HitData();
                        var hit = new MHit(data);
                        this.PopulateHitData(hit, target.Tile, arg);
                        hits.Add(hit);
                    }
                }
            }
            return hits;
        }

        protected List<CTile> GetRaycastTiles(AbilityArgs arg)
        {
            return this._logic.GetRaycastTiles(arg);
        }

        protected List<CTile> GetSelfCenteredTiles(MHit hit)
        {
            var cTiles = new List<CTile>();
            var tiles = hit.Data.Source.Tile.Model.GetAoETiles(this.Data.Range);
            foreach (var tile in tiles)
                cTiles.Add(tile.Controller);
            return cTiles;
        }

        protected virtual void PredictBullet(MHit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.PredictBullet(hit);
        }

        protected virtual void PredictMelee(MHit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.PredictMelee(hit);
        }

        protected void ProcessHitBullet(MHit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.ProcessBullet(hit);
        }

        protected void ProcessHitLoS(MHit hit)
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

        protected void ProcessHitMelee(MHit hit)
        {
            this.ProcessEffects(hit);
            this.ProcessPerks(hit);
            this._logic.ProcessMelee(hit);
        }

        protected void ProcessHitZone(MHit hit)
        {
            //foreach (var perk in hit.Source.Model.Perks.AbilityModPerks)
            //    perk.TryModAbility(hit);
        }

        private void PopulateHitData(MHit hit, CTile tile, AbilityArgs args)
        {
            hit.Data.Ability = this;
            hit.Data.Action = this.Data.ParentAction;
            hit.Data.IsHeal = this.Data.IsHeal;
            hit.Data.Source = args.Source;
            hit.Data.Target = tile;
        }

        private void ProcessEffects(MHit hit)
        {
            var proxy = hit.Data.Source.Proxy;
            foreach(var effect in this._data.Effects)
                effect.TryProcessHit(hit);
            if (proxy.GetLWeapon() != null)
            {
                foreach (var effect in proxy.GetLWeapon().Model.Data.Effects)
                    effect.TryProcessHit(hit);
            }
            if (proxy.GetRWeapon() != null)
            {
                foreach (var effect in proxy.GetRWeapon().Model.Data.Effects)
                    effect.TryProcessHit(hit);
            }
        }

        private void ProcessPerks(MHit hit)
        {
            foreach (var perk in hit.Data.Source.Proxy.GetPerks().GetAbilityModPerks())
                perk.TryModAbility(hit);
        }
    }
}
