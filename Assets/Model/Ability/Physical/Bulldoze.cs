using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Template.Script;
using Assets.View;
using Assets.View.Character;
using UnityEngine;

namespace Assets.Model.Ability.Physical
{
    public class Bulldoze : MAbility
    {
        public Bulldoze() : base(EAbility.Bulldoze) { }

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
            this.HandleSource(a);
            this.HandleTarget(a);
        }

        private void HandleSource(MAction a)
        {
            var data = new EvTileMoveData();
            data.Char = a.Data.Source;
            data.Cost = 0;
            data.Source = a.Data.Source.Tile;
            data.Target = a.Data.Target;
            var e = new EvTileMove(data);
            e.AddCallback(this.SourceCallback);
            foreach (var hit in a.Data.Hits)
                e.AddCallback(hit.CallbackHandler);
            e.TryProcess();
            var bob = a.Data.Source.Handle.GetComponent<SBob>();
            if (bob != null)
            {
                GameObject.Destroy(bob);
            }
        }

        private void HandleTarget(MAction a)
        {
            var tgt = a.Data.Target.Current as CChar;
            var data = new EvTileMoveData();
            data.Char = tgt;
            data.Cost = 0;
            data.Source = tgt.Tile;
            data.Target = a.Data.Source.Tile;
            var e = new EvTileMove(data);
            VCharUtil.Instance.AssignPlusLayer(tgt);
            e.AddCallback(this.TargetCallback);
            e.TryProcess();
        }

        private void SourceCallback(object o)
        {
            var e = o as EvTileMove;
            var bob = e.GetData().Char.Handle.AddComponent<SBob>();
            bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, e.GetData().Char.Handle);
        }

        private void TargetCallback(object o)
        {
            var e = o as EvTileMove;
            VCharUtil.Instance.UnassignPlusLayer(e.GetData().Char);
        }
    }
}
