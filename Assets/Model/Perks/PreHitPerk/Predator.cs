using Model.Characters;
using Model.Combat;

namespace Model.Perks
{
    public class Predator : GenericPreHitPerk
    {
        public Predator() : base(PerkEnum.Predator)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            base.TryModHit(hit);
            var injuries = hit.Target.Model.Injuries.Count;
            hit.Ability.DamageMod += (injuries * this.Val);
        }
    }
}
