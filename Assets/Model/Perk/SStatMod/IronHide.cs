using Assets.Model.Character.Enum;
using Model.Characters;

namespace Assets.Model.Perk.SStatMod
{
    public class IronHide : MSStatModPerk
    {
        public IronHide() : base(EPerk.Iron_Hide)
        {

        }

        public override void TryModSStat(ESecondaryStat stat, ref double value)
        {
            if (stat == ESecondaryStat.Damage_Reduction)
                value *= this.Val;
        }
    }
}
