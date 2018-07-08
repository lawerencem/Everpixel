using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Tile;
using Assets.Model.Character.Enum;
using Assets.Model.Map.Tile;
using Assets.Template.Pathing;
using Assets.Template.Script;
using Assets.View;
using UnityEngine;

namespace Assets.Model.Event.Combat
{
    public class EvPathMoveData
    {
        public CChar Char { get; set; }
        public CTile Source { get; set; }
        public CTile Target { get; set; }
        public Path TargetPath { get; set; } 
    }

    public class EvPathMove : MEvCombat
    {
        private CTile _current;
        private EvPathMoveData _data;
        private CTile _next;

        public EvPathMove() : base(ECombatEv.NewTurn) { }
        public EvPathMove(EvPathMoveData d) : base(ECombatEv.NewTurn) { this._data = d; }

        public void SetData(EvPathMoveData data) { this._data = data;}

        public override void TryProcess()
        {
            GUIManager.Instance.SetInteractionLocked(true);
            base.TryProcess();
            this.TryProcessPathMove();
        }

        public void AddBob(object o)
        {
            if (this._data.Char != null && this._data.Char.Equals(CombatManager.Instance.GetCurrentlyActing()))
            {
                var bob = this._data.Char.GameHandle.GetComponent<SBob>();
                if (bob == null)
                {
                    bob = this._data.Char.GameHandle.AddComponent<SBob>();
                    bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Char.GameHandle);
                }
            }
        }

        private void CallbackHandler()
        {
            GUIManager.Instance.SetGUILocked(false);
            GUIManager.Instance.SetInteractionLocked(false);
            CombatManager.Instance.SetCurrentAbilityNone();
            this.DoCallbacks();
        }

        private bool TryProcessPathMove()
        {
            if (this.VerifyAndPopulateData())
            {
                this.TryProcessFirstTile();
                return true;
            }
            else
            {
                this.CallbackHandler();
                return false;
            }
        }

        private void TileMoveDone(object o)
        {
            var e = o as EvTileMove;
            this._current = this._next;
            if (e.GetPathInterrupted())
                this.CallbackHandler();
            else
                this.TryProcessNextTile(this._current);
        }

        private void TryProcessFirstTile()
        {
            if (this._data.Source != null)
            {
                var ap = this._data.Char.Proxy.GetPoints(ESecondaryStat.AP);
                var stamina = this._data.Char.Proxy.GetPoints(ESecondaryStat.Stamina);
                var tile = this._data.TargetPath.GetFirstTile() as MTile;
                this._next = tile.Controller;
                var apCost = this._next.Model.GetCost();
                var stamCost = this._next.Model.GetStaminaCost();
                if (apCost <= ap && stamCost <= stamina)
                {
                    var data = new EvTileMoveData();
                    data.Cost = apCost;
                    data.Char = this._data.Char;
                    data.ParentMove = this;
                    data.Source = this._data.Source;
                    data.Target = this._next;
                    var e = new EvTileMove(data);
                    e.AddCallback(this.TileMoveDone);
                    e.TryProcess();
                }
                else
                    this.CallbackHandler();
            }
        }

        private void TryProcessNextTile(CTile tile)
        {
            var ap = this._data.Char.Proxy.GetPoints(ESecondaryStat.AP);
            var stamina = this._data.Char.Proxy.GetPoints(ESecondaryStat.Stamina);
            var model = (MTile) this._data.TargetPath.GetNextTile(tile.Model);
            if (model != null)
            {
                this._next = model.Controller;
                var apCost = this._next.Model.GetCost();
                var stamCost = this._next.Model.GetStaminaCost();
                if (apCost <= ap && stamCost <= stamina)
                {
                    var data = new EvTileMoveData();
                    data.Cost = apCost;
                    data.Char = this._data.Char;
                    data.ParentMove = this;
                    data.Source = this._current;
                    data.StamCost = this._data.Target.Model.GetStaminaCost();
                    data.Target = this._next;
                    var e = new EvTileMove(data);
                    e.AddCallback(this.TileMoveDone);
                    e.TryProcess();
                }
                else
                    this.CallbackHandler();
            }
            else
                this.CallbackHandler();
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Char == null)
                this._data.Char = CombatManager.Instance.GetCurrentlyActing();

            var bob = this._data.Char.GameHandle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
            this.AddCallback(this.AddBob);

            if (this._data.Target == null)
                return false;
            if (this._data.Source == null)
                this._data.Source = this._data.Char.Tile;

            var s = this._data.Source.Model;
            var t = this._data.Target.Model;
            var pathSearch = new PathSearch();
            this._data.TargetPath = pathSearch.GetPath(s, t, this._data.Char.Proxy.GetModel());
            if (this._data.TargetPath == null)
                return false;
            var model = this._data.TargetPath.GetFirstTile() as MTile;
            if (model == null)
                return false;
            this._current = model.Controller;
            return true;
        }
    }
}
