using Model.Combat;

namespace Model.Abilities
{
    public class Hadoken : GenericActiveAbility
    {
        public Hadoken() : base(ActiveAbilitiesEnum.Hadoken)
        {
            this.CastType = AbilityCastTypeEnum.Bullet;
            this.MagicType = Magic.MagicTypeEnum.Fighting;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessBullet(hit);
        }
    }
}
