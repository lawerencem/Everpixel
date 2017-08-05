using Model.Combat;
using Model.Events.Combat;

namespace Model.Perks
{
    public class GenericOnActionPerk : GenericPerk
    {
        public GenericOnActionPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryProcessAction(Hit hit)
        {

        }
    }
}
