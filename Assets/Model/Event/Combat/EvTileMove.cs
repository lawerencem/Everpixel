using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Controller.Script;
using Assets.View;

namespace Assets.Model.Event.Combat
{
    public class EvTileMoveData
    {
        public CChar Char { get; set; }
        public int Cost { get; set; }
        public CTile Source { get; set; }
        public CTile Target { get; set; }
    }

    public class EvTileMove : MEvCombat
    {
        private EvTileMoveData _data;

        public EvTileMove() : base(ECombatEv.TileMove) { }
        public EvTileMove(EvTileMoveData d) : base(ECombatEv.TileMove) { this._data = d; }

        public void SetData(EvTileMoveData d) { this._data = d; }

        public override void TryProcess()
        {
            base.TryProcess();
            this.TryProcessMove();       
        }

        private void MoveDone(object o)
        {
            var data = new EvAPModData();
            data.Char = this._data.Char;
            data.IsHeal = false;
            data.Qty = this._data.Cost;
            data.ToDisplay = false;
            var e = new EvAPMod(data);
            e.TryProcess();
            if (this._data.Char != null)
                this._data.Char.SetTile(this._data.Target);
            this._data.Source.SetCurrent(null);
            this._data.Target.SetCurrent(this._data.Char);
            this.DoCallbacks();
        }

        private bool VerifyData()
        {
            if (this._data == null)
                return false;
            else if (this._data.Char == null)
                return false;
            else if (this._data.Source == null)
                return false;
            else if (this._data.Target == null)
                return false;
            return true;
        }

        private bool TryProcessMove()
        {
            if (this.VerifyData())
            {
                var script = this._data.Char.Handle.AddComponent<SObjectMove>();
                var data = new SObjectMoveData();
                data.Epsilon = ViewParams.MOVE_EPSILON;
                data.Object = this._data.Char.Handle;
                data.Source = this._data.Source.Handle.transform.position;
                data.Speed = ViewParams.MOVE_SPEED;
                data.Target = this._data.Target.Handle.transform.position;
                script.Init(data);
                script.AddCallback(this.MoveDone);
                return true;
            }
            return false;
        }
    }
}
