using Model.Combat;

namespace Model.Perks
{
    public class GenericPostHitPerk : GenericPerk
    {
        public GenericPostHitPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryProcessAction(HitInfo hit)
        {

        }
    }
}
