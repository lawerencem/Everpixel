using Model.Abilities;
using Model.Combat;

namespace Model.Perks
{
    public class ShieldHappy : GenericPreHitPerk
    {
        public ShieldHappy() : base(PerkEnum.Shield_Happy)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            base.TryModHit(hit);
            if (hit.Target.Model.Shields.Count > 0)
            {
                hit.ModData.BlockMod = this.Val; // TODO: 2
            }
        }
    }
}
