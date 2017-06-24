using Model.Combat;

namespace Model.Perks
{
    public class GenericOnHitPerk : GenericPerk
    {
        public GenericOnHitPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModHit(HitInfo hit)
        {

        }
    }
}
