using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PostHit
{
    public class MPostHitPerk : MPerk
    {
        public MPostHitPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAction(Hit hit)
        {

        }
    }
}
