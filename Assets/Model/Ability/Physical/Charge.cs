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
    public class Charge : MAbility
    {
        public Charge() : base(EAbility.Charge) { }

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
            this.InitCharge(a);        
        }

        private void InitCharge(MAction a)
        {
            VCharUtil.Instance.AssignPlusLayer(a.Data.Source);
            var bob = a.Data.Source.GameHandle.GetComponent<SBob>();
            if (bob != null)
                GameObject.Destroy(bob);
            var data = new SRaycastMoveData();
            data.Epsilon = 0.15f;
            data.Handle = a.Data.Source.GameHandle;
            data.Speed = 3f;
            data.Target = a.Data.Target.Handle.transform.position;
            var script = a.Data.Source.GameHandle.AddComponent<SRaycastMove>();
            script.Init(data);
            script.AddObjectToList(a);
            script.AddCallback(this.ChargeDone);
        }

        private void ChargeDone(object o)
        {
            var raycast = o as SRaycastMove;
            var action = raycast.GetObjectList()[0] as MAction;
            var tgt = action.Data.Target.Current as CChar;
            var data = new SXAxisShakeData();
            data.Duration = 1.5f;
            data.MaxDistance = 0.01f;
            data.Speed = 0.75f;
            data.Target = tgt.GameHandle;
            var shake = tgt.GameHandle.AddComponent<SXAxisShake>();
            shake.Init(data);
            this.ProcessMove(action);
        }

        private void ProcessMove(MAction action)
        {
            var tgt = action.Data.Target.Current as CChar;
            var tile = tgt.Tile.GetRandomNearbyEmptyTile(1);
            if (tile == null)
                tile = tgt.Tile.GetNearestEmptyTile();
            var data = new EvTileMoveData();
            data.Char = action.Data.Source;
            data.Cost = 0;
            data.Source = action.Data.Source.Tile;
            data.Target = tile;
            var e = new EvTileMove(data);
            e.AddCallback(this.SourceCallback);
            foreach (var hit in action.Data.Hits)
                e.AddCallback(hit.CallbackHandler);
            e.TryProcess();
        }

        private void SourceCallback(object o)
        {
            var e = o as EvTileMove;
            var bob = e.GetData().Char.GameHandle.AddComponent<SBob>();
            bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, e.GetData().Char.GameHandle);
        }
    }
}
