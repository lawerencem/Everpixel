using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Shove : GenericAbility
    {
        public Shove() : base(AbilitiesEnum.Shove)
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
