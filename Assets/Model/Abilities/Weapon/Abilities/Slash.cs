using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Slash : GenericAbility
    {
        public Slash() : base(AbilitiesEnum.Slash)
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
