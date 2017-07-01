using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class ShieldWall : GenericAbility
    {
        public ShieldWall() : base(AbilitiesEnum.Shield_Wall)
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
