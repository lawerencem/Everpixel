using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class IndefSecondaryStatModifier : GIndefStatMod<ESecondaryStat>
    {
        public IndefSecondaryStatModifier(ESecondaryStat t, double scalar) : base(t, scalar)
        {

        }
    }
}
