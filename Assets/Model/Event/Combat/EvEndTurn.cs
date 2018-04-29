using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat;
using Assets.Template.Script;

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
                var bob = character.GameHandle.GetComponent<SBob>();
                if (bob != null)
                {
                    bob.Reset();
                }
            }
            VMapCombatController.Instance.ClearDecoratedTiles(this);
            CombatManager.Instance.ProcessEndTurn();
            this.DoCallbacks();
        }
    }
}
