using Model.Characters;

namespace Characters.Params
{
    public class PrimaryStatModifier : GenericStatModifier<PrimaryStatsEnum>
    {
        PrimaryStatModifier(PrimaryStatsEnum t, int dur, double scalar) : base(t, dur, scalar)
        {

        }
    }
}
