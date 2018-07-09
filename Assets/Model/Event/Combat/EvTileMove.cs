using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Controller.Script;
using Assets.Template.Event;
using Assets.View;

namespace Assets.Model.Event.Combat
{
    public class EvTileMoveData
    {
        public CChar Char { get; set; }
        public int Cost { get; set; }
        public bool DoAttackOfOpportunity { get; set; }
        public EvPathMove ParentMove { get; set; }
        public CTile Source { get; set; }
        public int StamCost { get; set; }
        public CTile Target { get; set; }

        public EvTileMoveData()
        {
            this.DoAttackOfOpportunity = true;
        }
    }

    public class EvTileMove : MEvCombat, IChildEvent
    {
        private bool _completed;
        private EvTileMoveData _data;
        private bool _pathInterrupted;

        public EvTileMove() : base(ECombatEv.TileMove) { }
        public EvTileMove(EvTileMoveData d) : base(ECombatEv.TileMove)
        {
            this._data = d;
            this._pathInterrupted = false;
            if (this._data.ParentMove != null)
                this._data.ParentMove.AddChildAction(this);
        }

        public EvTileMoveData GetData() { return this._data; }
        public bool GetPathInterrupted() { return this._pathInterrupted; }
        public EvPathMove GetParentMove() { return this._data.ParentMove; }
        public void SetData(EvTileMoveData d) { this._data = d; }
        public void SetPathInterrupted(object o) { this._pathInterrupted = true; }

        public override void TryProcess()
        {
            base.TryProcess();
            this.TryProcessMove();       
        }

        public bool GetCompleted()
        {
            return this._completed;
        }

        public override void TryDone(object o)
        {
            bool done = true;
            foreach (var action in this._childActions)
            {
                if (!action.GetCompleted())
                    done = false;
            }
            if (done)
            {
                this._completed = true;
                this.DoCallbacks();
            }
        }

        private void MoveDone(object o)
        {   
            if (!this._pathInterrupted)
            {
                this._data.Char.ProcessEnterNewTile(this._data.Target);
                this._data.Source.SetCurrent(null);
                this._data.Target.SetCurrent(this._data.Char);
                var moveData = new TileMoveData();
                moveData.Callback = this.SetPathInterrupted;
                moveData.ParentEvent = this;
                moveData.Target = this._data.Char;
                this._data.Target.ProcessEnterTile(moveData);
                this.TryDone(null);
            }
            else
                this.TryDone(null);
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

        private void TryProcessMove()
        {
            if (this.VerifyData())
            {
                var apData = new EvAPModData();
                apData.Char = this._data.Char;
                apData.IsHeal = false;
                apData.Qty = this._data.Cost;
                apData.ToDisplay = false;
                var apEvent = new EvAPMod(apData);
                apEvent.TryProcess();
                var staminaData = new EvStaminaModData();
                staminaData.Char = this._data.Char;
                staminaData.IsHeal = false;
                staminaData.Qty = this._data.Cost;
                staminaData.ToDisplay = false;
                var staminaEvent = new EvStaminaMod(staminaData);
                staminaEvent.TryProcess();

                var exitData = new TileMoveData();
                exitData.Callback = this.SetPathInterrupted;
                exitData.DoAttackOfOpportunity = this._data.DoAttackOfOpportunity;
                exitData.ParentEvent = this;
                exitData.Target = this._data.Char;
                this._data.Source.ProcessExitTile(exitData);

                if (!this._pathInterrupted)
                {
                    var script = this._data.Char.GameHandle.AddComponent<SObjectMove>();
                    var data = new SObjectMoveData();
                    data.Epsilon = ViewParams.MOVE_EPSILON;
                    data.Object = this._data.Char.GameHandle;
                    data.Source = this._data.Source.Handle.transform.position;
                    data.Speed = ViewParams.MOVE_SPEED;
                    data.Target = this._data.Target.Handle.transform.position;
                    script.Init(data);
                    script.AddCallback(this.MoveDone);
                }
                else
                    this.MoveDone(null);
            }
        }
    }
}
