using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class FlatSecondaryStatModifier : GStatMod<ESecondaryStat>
    {
        public int FlatMod { get; set; }

        public FlatSecondaryStatModifier(ESecondaryStat t, int dur, int flat) : base(t, dur, 1)
        {
            this.FlatMod = flat;
        }

        public void TryFlatScaleStat(ESecondaryStat t, ref double v)
        {
            if (t == this._type)
                v += this.FlatMod;
        }
    }
}
