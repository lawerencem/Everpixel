using Model.Combat;

namespace Model.Abilities
{
    public class EldrtichChomp : GenericActiveAbility
    {
        public EldrtichChomp() : base(ActiveAbilitiesEnum.Eldritch_Chomp)
        {
            this.CastType = AbilityCastTypeEnum.No_Collision_Bullet;
            this.MagicType = Magic.MagicTypeEnum.Astral;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessBullet(hit);
        }
    }
}
