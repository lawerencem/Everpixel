using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class WideSlash : GenericAbility
    {
        public WideSlash() : base(AbilitiesEnum.Wide_Slash)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
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
