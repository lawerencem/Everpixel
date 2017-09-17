using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Model.Character.Enum;

namespace Assets.Model.Event.Combat
{
    public class EvCharKilledData
    {
        public CChar Target { get; set; }
    }

    public class EvCharKilled : MEvCombat
    {
        private EvCharKilledData _data;

        public EvCharKilled() : base(ECombatEv.CharKilled) { }
        public EvCharKilled(EvCharKilledData d) : base(ECombatEv.CharKilled) { this._data = d; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.Initialized())
                this.Process();
        }

        private bool Initialized()
        {
            if (this._data != null && this._data.Target != null)
                return true;
            else
                return false;
        }

        private void Process()
        {
            this._data.Target.Tile.SetCurrent(null);
            this._data.Target.Tile.AddNonCurrent(this._data.Target);
            FCharacterStatus.SetDeadTrue(this._data.Target.Proxy.GetFlags());
            CombatManager.Instance.ProcessCharDeath(this._data.Target);
        }
    }
}
