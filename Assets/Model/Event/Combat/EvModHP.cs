using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Event.Combat
{
    public class EvModHPData
    {
        public int Dmg { get; set; }
        public FHit Flags { get; set; }
        public MHit Hit { get; set; }
        public bool IsFatality { get; set; }
        public bool IsHeal { get; set; }
        public CChar Target { get; set; }
    }

    public class EvModHP : MEvCombat
    {
        private EvModHPData _data;

        public EvModHP() : base(ECombatEv.ModifyHP) { }
        public EvModHP(EvModHPData data) : base(ECombatEv.ModifyHP) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsInitialized())
            {
                if (this._data.Target != null)
                    this._data.Target.Proxy.ModifyPoints(ESecondaryStat.HP, this._data.Dmg, this._data.IsHeal);
            }
            this.TryDone(null);
        }

        private bool IsInitialized()
        {
            if (this._data.Target == null || this._data.Flags == null)
                return false;
            return true;
        }
    }
}
