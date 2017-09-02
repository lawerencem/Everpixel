using Assets.Controller.Character;
using Assets.View.Character;

namespace Assets.View.Event
{
    public class EvCharDeathData
    {
        public CharController Target { get; set; }
    }

    public class EvCharDeath : MGuiEv
    {
        private EvCharDeathData _data;

        public EvCharDeath() : base(EGuiEv.CharDeath) { }
        public EvCharDeath(EvCharDeathData d) : base(EGuiEv.CharDeath) { this._data = d; }

        public void SetData(EvCharDeathData data) { this._data = data; }

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
            VCharUtil.Instance.ProcessDeadCharLayers(this._data.Target);
            var data = new EvSplatterData();
            data.Target = this._data.Target.Tile;
            data.DmgPercent = 1.0;
            var e = new EvSplatter(data);
            e.TryProcess();
        }
    }
}
