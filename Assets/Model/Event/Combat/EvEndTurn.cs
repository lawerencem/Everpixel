using Assets.Controller.Manager.Combat;
using Template.Script;

namespace Assets.Model.Event.Combat
{
    public class EvEndTurn : MEvCombat
    {
        public EvEndTurn() : base(ECombatEv.EndTurn) {}

        public override void TryProcess()
        {
            base.TryProcess();
            var character = CombatManager.Instance.GetCurrentlyActing();
            if (character != null)
            {
                var bob = character.Handle.GetComponent<SBob>();
                if (bob != null)
                {
                    bob.Reset();
                }
            }
            CombatManager.Instance.ProcessEndTurn();
        }
    }
}
