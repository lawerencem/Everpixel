using Model.Combat;

namespace Model.Perks
{
    public class GenericWhenHitPerk : GenericPerk
    {
        public GenericWhenHitPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModHit(Hit hit)
        {

        }
    }
}
