using Model.Characters;

namespace Model.Perks
{
    public class IronHide : GenericSStadModPerk
    {
        public IronHide() : base(PerkEnum.Iron_Hide)
        {

        }

        public override void TryModSStat(SecondaryStatsEnum stat, ref double value)
        {
            if (stat == SecondaryStatsEnum.Damage_Reduction)
                value *= this.Val;
        }
    }
}
