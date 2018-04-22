using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Template.Script;
using Assets.View.Character;

namespace Assets.View.Event
{
    public class EvViewCharDeathData
    {
        public CChar Target { get; set; }
    }

    public class EvViewCharDeath : MGuiEv
    {
        private EvViewCharDeathData _data;

        public EvViewCharDeath() : base(EGuiEv.CharDeath) { }
        public EvViewCharDeath(EvViewCharDeathData d) : base(EGuiEv.CharDeath) { this._data = d; }

        public void SetData(EvViewCharDeathData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsValid())
                this.Process();
        }

        private bool IsValid()
        {
            if (this._data != null && this._data.Target != null)
                return true;
            else
                return false;
        }

        private void Process()
        {
            VCharUtil.Instance.ProcessDeadChar(this._data.Target);
            if (this._data.Target.Equals(CombatManager.Instance.GetCurrentlyActing()))
            {
                var bob = this._data.Target.GameHandle.GetComponent<SBob>();
                if (bob != null)
                    bob.Reset();
                CombatManager.Instance.ProcessEndTurn();
            }
            var data = new EvSplatterData();
            data.Target = this._data.Target.Tile.Handle;
            data.DmgPercent = 0.5;
            var e = new EvSplatter(data);
            e.TryProcess();
            this.DoCallbacks();
        }
    }
}
