using Assets.Controller.Character;
using Assets.Model.Barrier;

namespace Assets.Model.Event.Combat
{
    public class EvBarrierData
    {
        public MBarrier barrier { get; set; }
        public CChar target { get; set; }
    }

    public class EvBarrier : MEvCombat
    {
        private EvBarrierData _data;

        public EvBarrier() : base(ECombatEv.Barrier) { }
        public EvBarrier(EvBarrierData data): base(ECombatEv.Barrier) { this._data = data; }

        public void SetData(EvBarrierData data) { this._data = data; }

        public override void TryProcess()
        {
            if (this.VerifyData())
            {
                base.TryProcess();
                this._data.target.Proxy.AddBarrier(this._data.barrier);
            }
        }

        private bool VerifyData()
        {
            if (this._data == null)
                return false;
            else if (this._data.barrier == null)
                return false;
            else if (this._data.target == null)
                return false;
            return true;
        }
    }
}
