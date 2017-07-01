using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class EldritchChomp : GenericAbility
    {
        public EldritchChomp() : base(AbilitiesEnum.Eldritch_Chomp)
        {
            this.CastType = AbilityCastTypeEnum.No_Collision_Bullet;
            this.MagicType = Magic.MagicTypeEnum.Astral;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessBullet(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
