using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.SStatMod
{
    public class ShieldPro : MSStatModPerk
    {
        public ShieldPro() : base(EPerk.Shield_Pro)
        {

        }

        public override void TryModSStat(ESecondaryStat stat, ref double value)
        {
            if (stat == ESecondaryStat.Block)
                value *= this.Val;
        }
    }
}
