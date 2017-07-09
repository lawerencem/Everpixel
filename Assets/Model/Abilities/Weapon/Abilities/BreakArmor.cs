using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class BreakArmor : GenericAbility
    {
        public BreakArmor() : base(AbilitiesEnum.Break_Armor)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictMelee(hit);
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
