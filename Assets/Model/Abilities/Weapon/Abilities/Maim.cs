using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Maim : GenericAbility
    {
        public Maim() : base(AbilitiesEnum.Maim)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessMelee(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
