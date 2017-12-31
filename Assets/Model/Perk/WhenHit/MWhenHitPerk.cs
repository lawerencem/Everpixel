using Assets.Model.Character.Param;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.WhenHit
{
    public class MWhenHitPerk : MPerk
    {
        public MWhenHitPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetWhenHitPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetWhenHitPerks().Add(this);
        }

        public virtual void TryModHit(MHit hit)
        {

        }
    }
}
