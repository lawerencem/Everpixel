using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class GreatStrike : GenericAbility
    {
        public GreatStrike() : base(AbilitiesEnum.Great_Strike)
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
