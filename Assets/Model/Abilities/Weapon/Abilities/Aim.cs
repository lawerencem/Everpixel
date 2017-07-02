using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Aim : GenericAbility
    {
        public Aim() : base(AbilitiesEnum.Aim)
        {
            this.CastType = AbilityCastTypeEnum.Bullet;
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
