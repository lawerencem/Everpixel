using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Stun : GenericAbility
    {
        public Stun() : base(AbilitiesEnum.Stun)
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
