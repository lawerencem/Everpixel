using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Pierce : GenericAbility
    {
        public Pierce() : base(AbilitiesEnum.Pierce)
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
