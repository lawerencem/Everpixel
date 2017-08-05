using Assets.Model.Ability.Enum;
using Model.Combat;
using Model.Events.Combat;

namespace Assets.Model.Ability
{
    public class Hadoken : Ability
    {
        public Hadoken() : base(EnumAbility.Hadoken)
        {

        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessBullet(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
