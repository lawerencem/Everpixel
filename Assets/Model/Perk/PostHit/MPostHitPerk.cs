using Assets.Model.Character.Param;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PostHit
{
    public class MPostHitPerk : MPerk
    {
        public MPostHitPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetPostHitPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetPostHitPerks().Add(this);
        }

        public virtual void TryProcessAction(MHit hit)
        {

        }
    }
}
