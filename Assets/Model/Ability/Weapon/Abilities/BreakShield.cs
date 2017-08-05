using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class BreakShield : GenericAbility
    {
        public BreakShield() : base(AbilitiesEnum.Break_Shield)
        {
            this.CastType = CastTypeEnum.Melee;
        }

        public override void PredictAbility(Hit hit)
        {
            base.PredictMelee(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, Hit hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessMelee(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
