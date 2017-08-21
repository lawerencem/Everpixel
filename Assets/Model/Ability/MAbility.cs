using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability
{
    public class MAbility : AAbility
    {
        public MAbility(EAbility type)
        {
            this.Params = new AbilityParamContainer();
            this._type = type;
        }
    }
}
