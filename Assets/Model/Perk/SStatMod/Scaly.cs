using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.SStatMod
{
    public class Scaly : MSStatModPerk
    {
        public Scaly() : base(EPerk.Scaly)
        {

        }

        public override void TryModSStat(ESecondaryStat stat, ref double value)
        {
            if (stat == ESecondaryStat.Damage_Ignore)
                value += this.Val;
        }
    }
}
