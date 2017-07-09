using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Fire : GenericAbility
    {
        public Fire() : base(AbilitiesEnum.Fire)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
