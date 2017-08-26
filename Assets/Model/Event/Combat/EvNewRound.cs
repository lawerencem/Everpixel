using Assets.Controller.Manager.Combat;

namespace Assets.Model.Event.Combat
{
    public class EvNewRound : MEvCombat
    {
        public EvNewRound() : base(ECombatEv.NewRound) { }

        public override void TryProcess()
        {
            base.TryProcess();
            CombatManager.Instance.ProcessNewRound();
        }
    }
}
