using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class SecondaryStatMod : GStatMod<ESecondaryStat>
    {
        public SecondaryStatMod(ESecondaryStat t, int dur, double scalar) : base(t, dur, scalar)
        {

        }
    }
}

