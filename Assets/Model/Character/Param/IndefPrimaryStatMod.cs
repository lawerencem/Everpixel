using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class IndefPrimaryStatMod : GIndefStatMod<EPrimaryStat>
    {
        public IndefPrimaryStatMod(EPrimaryStat t, double scalar) : base(t, scalar)
        {

        }
    }
}
