using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.SStatMod
{
    public class Massive : MSStatModPerk
    {
        public Massive() : base(EPerk.Massive)
        {

        }

        public override void TryModSStat(ESecondaryStat stat, ref double value)
        {
            if (stat == ESecondaryStat.HP)
                value *= this.Val;
        }
    }
}
