using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class ShieldWall : GenericAbility
    {
        public ShieldWall() : base(AbilitiesEnum.Shield_Wall)
        {
            this.CastType = CastTypeEnum.Melee;
        }

        public override void PredictAbility(Hit hit)
        {
            base.PredictMelee(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, Hit hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
