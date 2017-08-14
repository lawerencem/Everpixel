using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.SStatMod
{
    public class Gargantuan : MSStatModPerk
    {
        public Gargantuan() : base(EPerk.Gargantuan)
        {

        }

        public override void TryModSStat(ESecondaryStat stat, ref double value)
        {
            if (stat == ESecondaryStat.HP)
                value *= this.Val;
        }
    }
}
