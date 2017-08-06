using Assets.Model.Combat;

namespace Assets.Model.Perk.WhenHit
{
    public class MWhenHitPerk : MPerk
    {
        public MWhenHitPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryModHit(Hit hit)
        {

        }
    }
}
