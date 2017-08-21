namespace Assets.Model.Event.Combat
{
    public class MEvCombat : AEvCombat<ECombatEv>
    {
        public MEvCombat(ECombatEv t) : base(t)
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
