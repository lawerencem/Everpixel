using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Scatter : GenericAbility
    {
        public Scatter() : base(AbilitiesEnum.Scatter)
        {
            this.CastType = AbilityCastTypeEnum.Weapon;
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
