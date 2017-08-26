using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class Predator : MPreHitPerk
    {
        public Predator() : base(EPerk.Predator)
        {

        }

        public override void TryModHit(Hit hit)
        {
            //base.TryModHit(hit);
            //var injuries = hit.Target.Model.Injuries.Count;
            //hit.Ability.DamageMod += (injuries * this.Val);
        }
    }
}
