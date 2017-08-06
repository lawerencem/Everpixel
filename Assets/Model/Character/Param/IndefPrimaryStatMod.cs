using Assets.Model.Character.Enum;
using Model.Characters;

namespace Assets.Model.Character.Param
{
    public class IndefPrimaryStatMod : GIndefStatMod<EPrimaryStat>
    {
        public IndefPrimaryStatMod(EPrimaryStat t, double scalar) : base(t, scalar)
        {

        }
    }
}
