using Model.Characters;

namespace Characters.Params
{
    public class SecondaryStatModifier : GenericStatModifier<SecondaryStatsEnum>
    {
        SecondaryStatModifier(SecondaryStatsEnum t, int dur, double scalar) : base(t, dur, scalar)
        {

        }
    }
}

