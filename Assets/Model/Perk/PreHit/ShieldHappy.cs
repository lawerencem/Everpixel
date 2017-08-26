using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class ShieldHappy : MPreHitPerk
    {
        public ShieldHappy() : base(EPerk.Shield_Happy)
        {

        }

        public override void TryModHit(Hit hit)
        {
            //base.TryModHit(hit);
            //if (hit.Target.Model.Shields.Count > 0)
            //{
            //    hit.ModData.BlockMod = this.Val;
            //}
        }
    }
}
