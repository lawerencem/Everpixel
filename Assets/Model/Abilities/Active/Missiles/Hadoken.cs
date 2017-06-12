using Model.Combat;

namespace Model.Abilities
{
    public class Hadoken : GenericActiveAbility
    {
        public Hadoken() : base(ActiveAbilitiesEnum.Hadoken)
        {
            this.CastType = AbilityCastTypeEnum.Bullet;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessBullet(hit);
        }
    }
}
