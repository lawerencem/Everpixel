using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class PrimaryStatMod : GStatMod<EPrimaryStat>
    {
        PrimaryStatMod(EPrimaryStat t, int dur, double scalar) : base(t, dur, scalar)
        {

        }
    }
}
