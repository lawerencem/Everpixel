using Assets.Controller.Character;
using Assets.Controller.Map.Environment;
using Assets.Controller.Map.Tile;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Map.Tile;
using Assets.Template.Other;
using Assets.Template.Util;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class BulletLogic
    {
        private class StrayTargetData
        {
            public double Chance { get; set; }
            public CTile Tile { get; set; }
        }

        private readonly double BASE_ADJACENT_DIST_ONE_STRAY_CHANCE = 0.80;
        private readonly double BASE_ADJACENT_DIST_TWO_STRAY_CHANCE = 0.20;
        private readonly double BASE_ADJACENT_MULTIPLE = 6;
        private readonly double BASE_OBSTRUCTION_CHANCE = 0.05;
        private readonly double BASE_SHIELD_OBSTRUCTION_CHANCE = 0.10;
        private readonly double BASE_SHIELDWALL_OBSTRUCTION_CHANCE = 0.15;
        private readonly double BASE_STRAY_FLOOR = 750;
        private readonly double BASE_STRAY_SCALAR = 20;

        private double _currentStrayChance;
        private Dictionary<Pair<double, double>, StrayTargetData> _strayTargets;

        public BulletLogic()
        {
            this._currentStrayChance = 0;
            this._strayTargets = new Dictionary<Pair<double, double>, StrayTargetData>();
        }

        public void AttemptToStrayBullet(MHit hit)
        {
            this.SetBulletStrayChance(hit);
            this.CheckAdjacentObstructions(hit);
            this.TrySetStrayTarget(hit);
        }

        private void CheckAdjacentObstructions(MHit hit)
        {
            var tgt = hit.Data.Target.Current as CChar;   
            if (tgt.Tile.Model.GetCol() <= hit.Data.Source.Tile.Model.GetCol())
            {
                if (tgt.Tile.Model.GetRow() >= hit.Data.Source.Tile.Model.GetRow())
                    this.HandleQuadrantOne(hit, tgt);
                else
                    this.HandleQuadrantFour(hit, tgt);
            }
            else
            {
                if (tgt.Tile.Model.GetRow() >= hit.Data.Source.Tile.Model.GetRow())
                    this.HandleQuadrantTwo(hit, tgt);
                else
                    this.HandleQuadrantThree(hit, tgt);
            }
        }

        private void HandleCharacterOccupant(MTile tile)
        {
            var data = new StrayTargetData();
            data.Tile = tile.Controller;
            var occupant = tile.GetCurrentOccupant() as CChar;
            if (occupant.Proxy.Type == ECharType.Humanoid)
            {
                if (FActionStatus.HasFlag(occupant.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.ShieldWalling))
                    data.Chance = BASE_SHIELDWALL_OBSTRUCTION_CHANCE;
                else if (occupant.Proxy.GetLWeapon() != null && occupant.Proxy.GetLWeapon().IsTypeOfShield())
                    data.Chance = BASE_SHIELD_OBSTRUCTION_CHANCE;
                else if (occupant.Proxy.GetRWeapon() != null && occupant.Proxy.GetRWeapon().IsTypeOfShield())
                    data.Chance = BASE_SHIELD_OBSTRUCTION_CHANCE;
                else
                    data.Chance = BASE_OBSTRUCTION_CHANCE;
            }
            else
                data.Chance = BASE_OBSTRUCTION_CHANCE;
            var pair = new Pair<double, double>(tile.GetCol(), tile.GetRow());
            if (this._strayTargets.ContainsKey(pair))
                this._strayTargets[pair].Chance += data.Chance;
            else
                this._strayTargets.Add(pair, data);
        }

        private void HandleDecoOccupant(MTile tile)
        {
            var occupant = tile.GetCurrentOccupant() as CDeco;
            var data = new StrayTargetData();
            data.Chance = occupant.Model.GetBulletObstructionChance();
            data.Tile = tile.Controller;
            var pair = new Pair<double, double>(tile.GetCol(), tile.GetRow());
            if (this._strayTargets.ContainsKey(pair))
                this._strayTargets[pair].Chance += data.Chance;
            else
                this._strayTargets.Add(pair, data);
        }

        private void HandleQuadrantOne(MHit hit, CChar tgt)
        {
            var obstructionTiles = new List<MTile>();
            obstructionTiles.Add(tgt.Tile.Model.GetN());
            obstructionTiles.Add(tgt.Tile.Model.GetNE());
            obstructionTiles.Add(tgt.Tile.Model.GetSE());
            foreach (var tile in obstructionTiles)
            {
                if (tile != null)
                    this.HandleQuadrantHelper(hit, tgt, tile);
            }
        }

        private void HandleQuadrantTwo(MHit hit, CChar tgt)
        {
            var obstructionTiles = new List<MTile>();
            obstructionTiles.Add(tgt.Tile.Model.GetN());
            obstructionTiles.Add(tgt.Tile.Model.GetNW());
            obstructionTiles.Add(tgt.Tile.Model.GetSW());
            foreach (var tile in obstructionTiles)
            {
                if (tile != null)
                    this.HandleQuadrantHelper(hit, tgt, tile);
            }
        }

        private void HandleQuadrantThree(MHit hit, CChar tgt)
        {
            var obstructionTiles = new List<MTile>();
            obstructionTiles.Add(tgt.Tile.Model.GetS());
            obstructionTiles.Add(tgt.Tile.Model.GetSW());
            obstructionTiles.Add(tgt.Tile.Model.GetNW());
            foreach (var tile in obstructionTiles)
            {
                if (tile != null)
                    this.HandleQuadrantHelper(hit, tgt, tile);
            }
        }

        private void HandleQuadrantFour(MHit hit, CChar tgt)
        {
            var obstructionTiles = new List<MTile>();
            obstructionTiles.Add(tgt.Tile.Model.GetS());
            obstructionTiles.Add(tgt.Tile.Model.GetSE());
            obstructionTiles.Add(tgt.Tile.Model.GetNE());
            foreach (var tile in obstructionTiles)
            {
                if (tile != null)
                    this.HandleQuadrantHelper(hit, tgt, tile);
            }
        }

        private void HandleQuadrantHelper(MHit hit, CChar tgt, MTile tile)
        {
            if (tile.GetCurrentOccupant() != null)
            {
                if (tile.GetCurrentOccupant().GetType().Equals(typeof(CChar)))
                    this.HandleCharacterOccupant(tile);
                else if (tile.GetCurrentOccupant().GetType().Equals(typeof(CDeco)))
                    this.HandleDecoOccupant(tile);
            }
        }

        private void SetBulletStrayChance(MHit hit)
        {
            if (hit.Data.Source.Proxy.GetStat(ESecondaryStat.Ranged) < BASE_STRAY_FLOOR)
            {
                double delta = BASE_STRAY_FLOOR - hit.Data.Source.Proxy.GetStat(ESecondaryStat.Ranged);
                this._currentStrayChance = delta / BASE_STRAY_SCALAR / 100;

                foreach (var neighbor in hit.Data.Target.Model.GetAoETiles(1))
                {
                    var data = new StrayTargetData();
                    data.Tile = neighbor.Controller;
                    data.Chance = this._currentStrayChance * BASE_ADJACENT_DIST_ONE_STRAY_CHANCE / BASE_ADJACENT_MULTIPLE;
                    this._strayTargets.Add(new Pair<double, double>(neighbor.GetCol(), neighbor.GetRow()), data);
                }
                foreach (var neighbor in hit.Data.Target.Model.GetAoETiles(2))
                {
                    var pair = new Pair<double, double>(neighbor.GetCol(), neighbor.GetRow());
                    if (!this._strayTargets.ContainsKey(pair))
                    {
                        var data = new StrayTargetData();
                        data.Tile = neighbor.Controller;
                        data.Chance = this._currentStrayChance * BASE_ADJACENT_DIST_TWO_STRAY_CHANCE / BASE_ADJACENT_MULTIPLE;
                        this._strayTargets.Add(new Pair<double, double>(neighbor.GetCol(), neighbor.GetRow()), data);
                    }
                }
            }
        }

        private void TrySetStrayTarget(MHit hit)
        {
            double cumulative = 0;
            var roll = RNG.Instance.NextDouble();
            foreach (var stray in this._strayTargets)
            {
                cumulative += stray.Value.Chance;
                if (roll <= cumulative)
                {
                    hit.Data.Target = stray.Value.Tile;
                    break;
                }
            }
        }
    }
}
