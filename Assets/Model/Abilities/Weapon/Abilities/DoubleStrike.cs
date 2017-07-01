using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class DoubleStrike : GenericAbility
    {
        public DoubleStrike() : base(AbilitiesEnum.Double_Strike)
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
