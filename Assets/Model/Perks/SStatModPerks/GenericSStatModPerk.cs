using Assets.Generics;
using Characters.Params;
using Model.Characters;
using System.Collections.Generic;

namespace Model.Perks
{
    public class GenericSStadModPerk : GenericPerk
    {
        public GenericSStadModPerk(PerkEnum type) : base(type)
        {

        }

        public virtual void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {

        }
    }
}
