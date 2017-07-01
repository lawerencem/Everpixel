using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Aim : GenericAbility
    {
        public Aim() : base(AbilitiesEnum.Aim) { }

        public override void ProcessAbility(HitInfo hit)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
