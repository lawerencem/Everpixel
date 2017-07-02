using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class SpearWall : GenericAbility
    {
        public SpearWall() : base(AbilitiesEnum.Spear_Wall)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
