using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Fire : GenericAbility
    {
        public Fire() : base(AbilitiesEnum.Fire)
        {
            this.CastType = CastTypeEnum.Melee;
        }

        public override void PredictAbility(Hit hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, Hit hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
