using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Hadoken : GenericAbility
    {
        public Hadoken() : base(AbilitiesEnum.Hadoken)
        {
            this.CastType = AbilityCastTypeEnum.Bullet;
            this.MagicType = Magic.MagicTypeEnum.Fighting;
        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessBullet(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
