using Assets.Model.Character.Param;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class MPreHitPerk : MPerk
    {
        public MPreHitPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetPreHitPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetPreHitPerks().Add(this);
        }

        public virtual void TryModHit(MHit hit)
        {

        }
    }
}
