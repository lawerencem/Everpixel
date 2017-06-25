using Model.Characters;

namespace Characters.Params
{
    public class FlatSecondaryStatModifier : GenericStatModifier<SecondaryStatsEnum>
    {
        public int FlatMod { get; set; }

        public FlatSecondaryStatModifier(SecondaryStatsEnum t, int dur, int flat) : base(t, dur, 1)
        {
            this.FlatMod = flat;
        }

        public void TryFlatScaleStat(SecondaryStatsEnum t, ref double v)
        {
            if (t == this._type)
                v += this.FlatMod;
        }
    }
}

