using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Riposte : GenericAbility
    {
        public Riposte() : base(AbilitiesEnum.Riposte)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
