using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Fire : GenericAbility
    {
        public Fire() : base(AbilitiesEnum.Fire)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(HitInfo hit)
        {

        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
