using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.OnAction
{
    public class MOnActionPerk : MPerk
    {
        public MOnActionPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAction(MHit hit)
        {

        }
    }
}
