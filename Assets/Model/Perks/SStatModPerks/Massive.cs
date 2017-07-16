using Model.Characters;

namespace Model.Perks
{
    public class Massive : GenericSStadModPerk
    {
        public Massive() : base(PerkEnum.Massive)
        {

        }

        public override void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {
            if (stat == SecondaryStatsEnum.HP)
                value *= this.Val; // TODO: 1.25
        }
    }
}
