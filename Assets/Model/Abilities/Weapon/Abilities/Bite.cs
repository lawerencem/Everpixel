using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Bite : GenericAbility
    {
        public Bite() : base(AbilitiesEnum.Bite)
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
