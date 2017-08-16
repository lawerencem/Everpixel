using System;

namespace Assets.Model.Event.Combat
{
    public class MCombatEv : ACombatEv<ECombatEv>
    {
        public MCombatEv(ECombatEv t) : base(t)
        {

        }

        public override void Register()
        {
            this._manager.RegisterEvent(this);
        }

        public override void TryProcess()
        {
            base.TryProcess();
            this.Register();
        }
    }
}
