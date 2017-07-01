using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class GreatStrike : GenericAbility
    {
        public GreatStrike() : base(AbilitiesEnum.Great_Strike)
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
