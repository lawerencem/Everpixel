using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class DoubleStrike : GenericAbility
    {
        public DoubleStrike() : base(AbilitiesEnum.Double_Strike)
        {
            this.CastType = CastTypeEnum.Melee;
        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictMelee(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
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
