using Assets.Controller.Character;
using Assets.Controller.Manager;
using Assets.Controller.Map.Tile;
using Assets.Model.Map;

namespace Assets.Model.Event.Combat
{
    public class EvPathMoveData
    {
        public CharController Char { get; set; }
        public TileController Source { get; set; }
        public TileController Target { get; set; }
        public Path TargetPath { get; set; } 
    }

    public class EvPathMove : MCombatEv
    {
        private TileController _current;
        private EvPathMoveData _data;
        private TileController _next;

        public EvPathMove() : base(ECombatEv.TakingAction) { }
        public EvPathMove(EvPathMoveData d) : base(ECombatEv.TakingAction) { this._data = d; }

        public void SetData(EvPathMoveData data) { this._data = data;}

        public override void TryProcess()
        {
            base.TryProcess();
            this.TryProcessPathMove();
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
                var e = o as EvTileMove;
                this._current = this._next;
                this.TryProcessNextTile();
            }
        }

        private void TryProcessNextTile()
        {
            var ap = this._data.Char.Model.GetCurrentPoints().CurrentAP;
            this._next = this._data.TargetPath.GetNextTile(this._current);
            if (this._next != null)
            {
                var cost = this._data.Char.Model.GetTileTraversalAPCost(this._next.Model);
                if (cost <= ap)
                {
                    var data = new EvTileMoveData();
                    data.Char = this._data.Char;
                    data.Source = this._current;
                    data.Target = this._next;
                    var e = new EvTileMove(data);
                    e.AddCallback(this.TileMoveDone);
                    e.TryProcess();
                }
                else
                {
                    this.DoCallbacks();
                }
            }
            else
                this.DoCallbacks();
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            else if (this._data.Target == null)
                return false;

            if (this._data.Char == null)
                this._data.Char = CombatManager.Instance.GetCurrentlyActing();
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
