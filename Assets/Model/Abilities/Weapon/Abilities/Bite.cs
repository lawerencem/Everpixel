using Model.Combat;

namespace Model.Abilities
{
    public class Bite : WeaponAbility
    {
        public Bite() : base(WeaponAbilitiesEnum.Bite) { }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessMelee(hit);
        }
    }
}
