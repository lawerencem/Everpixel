using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class BreakShield : GenericAbility
    {
        public BreakShield() : base(AbilitiesEnum.Break_Shield)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessMelee(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
