using Model.Characters;

namespace Characters.Params
{
    public class SecondaryStatModifier : GenericStatModifier<SecondaryStatsEnum>
    {
        public SecondaryStatModifier(SecondaryStatsEnum t, int dur, double scalar) : base(t, dur, scalar)
        {

        }
    }
}

