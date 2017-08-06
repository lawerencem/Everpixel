using Assets.Model.Combat;
using Model.Combat;

namespace Assets.Model.Perk.PreHit
{
    public class MPreHitPerk : MPerk
    {
        public MPreHitPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryModHit(Hit hit)
        {

        }
    }
}
