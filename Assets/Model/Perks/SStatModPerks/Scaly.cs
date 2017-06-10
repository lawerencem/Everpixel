using Assets.Generics;
using Characters.Params;
using Model.Characters;
using System.Collections.Generic;

namespace Model.Perks
{
    public class Scaly : GenericSStadModPerk
    {
        public Scaly() : base(PerkEnum.Scaly)
        {

        }

        public override void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {
            if (stat == SecondaryStatsEnum.Damage_Ignore)
                value += 4;
        }
    }
}
