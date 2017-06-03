using Model.Characters;

namespace Characters.Params
{
    public class IndefPrimaryStatModifier : IndefGenericStatModifier<PrimaryStatsEnum>
    {
        public IndefPrimaryStatModifier(PrimaryStatsEnum t, double scalar) : base(t, scalar)
        {

        }
    }
}
