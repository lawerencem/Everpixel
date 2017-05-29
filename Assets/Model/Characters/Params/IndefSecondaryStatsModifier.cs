using Model.Characters;

namespace Characters.Params
{
    public class IndefSecondaryStatModifier : IndefGenericStatModifier<SecondaryStatsEnum>
    {
        public IndefSecondaryStatModifier(SecondaryStatsEnum t, double scalar) : base(t, scalar)
        {

        }
    }
}
