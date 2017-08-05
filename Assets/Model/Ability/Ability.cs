using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability
{
    public class Ability : AAbility
    {
        public Ability(EnumAbility type)
        {
            this.Params = new AbilityParamContainer();
        }
    }
}
