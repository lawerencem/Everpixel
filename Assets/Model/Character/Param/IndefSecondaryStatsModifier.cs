using Assets.Model.Character.Enum;
using Model.Characters;

namespace Assets.Model.Character.Param
{
    public class IndefSecondaryStatModifier : GIndefStatMod<ESecondaryStat>
    {
        public IndefSecondaryStatModifier(ESecondaryStat t, double scalar) : base(t, scalar)
        {

        }
    }
}
