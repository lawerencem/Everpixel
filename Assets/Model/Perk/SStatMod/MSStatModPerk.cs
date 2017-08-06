using Assets.Model.Character.Enum;

namespace Assets.Model.Perk.SStatMod
{
    public class MSStatModPerk : MPerk
    {
        public MSStatModPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryModSStat(ESecondaryStat stat, ref double value)
        {

        }
    }
}
