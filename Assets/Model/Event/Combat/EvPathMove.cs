using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Tile;
using Assets.Model.Character.Enum;
using Assets.Model.Map;
using Assets.Template.Script;
using Assets.View;

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

        public EvPathMove() : base(ECombatEv.TakingAction) { }
        public EvPathMove(EvPathMoveData d) : base(ECombatEv.TakingAction) { this._data = d; }

        public void SetData(EvPathMoveData data) { this._data = data;}

        public override void TryProcess()
        {
            GUIManager.Instance.SetInteractionLocked(true);
            base.TryProcess();
            this.TryProcessPathMove();
        }

        public void AddBob(object o)
        {
            if (this._data.Char != null)
            {
                var bob = this._data.Char.Handle.AddComponent<SBob>();
                bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Char.Handle);
            }
        }

        private bool ProcessMove()
        {
            this.TryProcessNextTile();
            return true;
        }

        private bool TryProcessPathMove()
        {
            if (this.VerifyAndPopulateData())
            {
                this.TryProcessNextTile();
                return true;
            }
            else
            {
                this.DoCallbacks();
                return false;
            }
        }

        private void TileMoveDone(object o)
        {
            if (o.GetType().Equals(typeof(EvTileMove)))
            {
                this._current = this._next;
                this.TryProcessNextTile();
            }
        }

        private void TryProcessNextTile()
        {
            var ap = this._data.Char.Proxy.GetStat(ESecondaryStat.AP);
            this._next = this._data.TargetPath.GetNextTile(this._current);
            if (this._next != null)
            {
                var cost = this._data.Char.Proxy.GetTileTraversalAPCost(this._next);
                if (cost <= ap)
                {
                    var data = new EvTileMoveData();
                    data.Cost = cost;
                    data.Char = this._data.Char;
                    data.Source = this._current;
                    data.Target = this._next;
                    var e = new EvTileMove(data);
                    e.AddCallback(this.TileMoveDone);
                    e.TryProcess();
                }
                else
                    this.DoCallbacks();
            }
            else
                this.DoCallbacks();
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Char == null)
                this._data.Char = CombatManager.Instance.GetCurrentlyActing();

            var bob = this._data.Char.Handle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
            this._callbacks.Add(this.AddBob);

            if (this._data.Target == null)
                return false;
            if (this._data.Source == null)
                this._data.Source = this._data.Char.Tile;

            this._current = this._data.Source;
            var s = this._data.Source.Model;
            var t = this._data.Target.Model;
            this._data.TargetPath = this._data.Target.Model.Map.GetPath(s, t);
            return true;
        }
    }
}
