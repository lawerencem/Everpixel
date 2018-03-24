using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Model.Shield;

namespace Assets.Model.Event.Combat
{
    public class EvShieldData
    {
        public MShield shield { get; set; }
        public CChar target { get; set; }
    }

    public class EvShield : MEvCombat
    {
        private EvShieldData _data;

        public EvShield() : base(ECombatEv.Shield) { }
        public EvShield(EvShieldData data): base(ECombatEv.Shield) { this._data = data; }

        public void SetData(EvShieldData data) { this._data = data; }

        public override void TryProcess()
        {
            if (this.VerifyData())
            {
                base.TryProcess();
                this._data.target.Proxy.AddShield(this._data.shield);
            }
        }

        private bool VerifyData()
        {
            if (this._data == null)
                return false;
            else if (this._data.shield == null)
                return false;
            else if (this._data.target == null)
                return false;
            return true;
        }
    }
}
