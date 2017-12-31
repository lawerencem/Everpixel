using Assets.Model.Character.Param;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.OnAction
{
    public class MOnActionPerk : MPerk
    {
        public MOnActionPerk(EPerk type) : base(type)
        {

        }

        public override void AddToParent(CharPerks parentContainer)
        {
            var exists = parentContainer.GetOnActionPerks().Find(x => x.Type == this.Type);
            if (exists == null)
                parentContainer.GetOnActionPerks().Add(this);
        }

        public virtual void TryProcessAction(MHit hit)
        {

        }
    }
}
