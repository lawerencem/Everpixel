using Model.Characters;

namespace Model.Perks
{
    public class Gargantuan : GenericSStadModPerk
    {
        public Gargantuan() : base(PerkEnum.Gargantuan)
        {

        }

        public override void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {
            if (stat == SecondaryStatsEnum.HP)
                value *= this.Val; // TODO: 1.35
        }
    }
}
