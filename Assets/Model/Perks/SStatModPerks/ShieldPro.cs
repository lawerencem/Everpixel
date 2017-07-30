using Model.Characters;

namespace Model.Perks
{
    public class ShieldPro : GenericSStadModPerk
    {
        public ShieldPro() : base(PerkEnum.Shield_Pro)
        {

        }

        public override void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {
            if (stat == SecondaryStatsEnum.Block)
                value *= this.Val;
        }
    }
}
