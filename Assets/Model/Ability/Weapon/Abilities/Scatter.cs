using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Model.Map.Tile;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.Model.Weapon.Abilities
{
    public class Scatter : MAbility
    {
        public Scatter() : base(EAbility.Scatter) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitMelee(hit);
        }

        public override void DisplayFX(MAction a)
        {
            base.DisplayFX(a);
            foreach (var hit in a.Data.Hits)
                hit.AddCallback(this.DoScatter);
            VHitController.Instance.ProcessMeleeHitFX(a);
        }

        public void DoScatter(object o)
        {
            var hit = o as MHit;
            var tgt = hit.Data.Target.Current as CChar;
            if (tgt != null && 
                FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Push) &&
                !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
            {
                CTile randomController = null;
                var tgtTile = hit.Data.Source.Tile.Model.GetPushTile(tgt.Tile.Model);
                if (tgtTile == null)
                    randomController = hit.Data.Source.Tile.GetNearestEmptyTile();
                if (randomController != null)
                    tgtTile = randomController.Model;
                if (tgtTile != null)
                {
                    var data = new EvTileMoveData();
                    data.Char = tgt;
                    data.Cost = 0;
                    data.DoAttackOfOpportunity = false;
                    data.StamCost = 0;
                    data.Source = tgt.Tile;
                    data.Target = tgtTile.Controller;
                    var e = new EvTileMove(data);
                    e.AddCallback(hit.CallbackHandler);
                    e.TryProcess();
                }

            }
        }
    }
}
