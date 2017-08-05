using Model.Combat;
using Model.Effects;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class MindBlast : GenericAbility
    {
        public MindBlast() : base(AbilitiesEnum.Mind_Blast)
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
