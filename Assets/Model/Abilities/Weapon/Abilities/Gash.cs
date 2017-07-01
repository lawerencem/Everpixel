using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Gash : GenericAbility
    {
        public Gash() : base(AbilitiesEnum.Gash)
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
