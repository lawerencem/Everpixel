using Assets.Controller.Manager;
using Template.Script;

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
