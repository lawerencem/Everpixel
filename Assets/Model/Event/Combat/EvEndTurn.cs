using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Combat;
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
            VMapCombatController.Instance.ClearDecoratedTiles(this);
            CombatManager.Instance.ProcessEndTurn();
        }
    }
}
