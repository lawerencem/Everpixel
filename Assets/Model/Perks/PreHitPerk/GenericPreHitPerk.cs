using Model.Combat;

namespace Model.Perks
{
    public class GenericPreHitPerk : GenericPerk
    {
        public GenericPreHitPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModHit(HitInfo hit)
        {

        }
    }
}
