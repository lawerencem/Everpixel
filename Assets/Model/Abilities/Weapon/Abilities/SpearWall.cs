using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class SpearWall : GenericAbility
    {
        public SpearWall() : base(AbilitiesEnum.Spear_Wall)
        {
            this.CastType = AbilityCastTypeEnum.Weapon;
        }

        public override void ProcessAbility(HitInfo hit)
        {

        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
