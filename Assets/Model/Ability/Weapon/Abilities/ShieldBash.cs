using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.View.Fatality;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.Model.Weapon.Abilities
{
    public class ShieldBash : MAbility
    {
        public ShieldBash() : base(EAbility.Shield_Bash) { this._wpnAbility = true; }

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
            var pos = Vector3.Lerp(
                a.Data.Source.GameHandle.transform.position,
                a.Data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var script = a.Data.Source.GameHandle.AddComponent<SAttackerJolt>();
            script.Action = a;
            script.AddCallback(this.DoBash);
            script.AddObjectToList(a);
            script.Init(a.Data.Source, pos, CombatGUIParams.ATTACK_SPEED);
        }

        public void DoBash(object o)
        {
            var script = o as SAttackerJolt;
            var a = script.GetObjectList()[0] as MAction;
            foreach (var hit in a.Data.Hits)
            {
                var tgt = hit.Data.Target.Current as CChar;
                if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Push))
                {
                    var tgtTile = hit.Data.Source.Tile.Model.GetPushTile(tgt.Tile.Model);
                    if (tgtTile != null)
                    {
                        var data = new EvTileMoveData();
                        data.Char = tgt;
                        data.Cost = 0;
                        data.StamCost = 0;
                        data.Source = tgt.Tile;
                        data.Target = tgtTile.Controller;
                        var e = new EvTileMove(data);
                        e.AddCallback(hit.CallbackHandler);
                        e.TryProcess();
                    }
                }
                else
                    hit.CallbackHandler(null);
            }
        }
    }
}
