using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic;

namespace Assets.Model.Ability
{
    public class MAbility : AAbility
    {
        public MAbility(EAbility type)
        {
            this.Params = new AbilityParamContainer();
            this._logic = new AbilityLogic();
            this._type = type;
        }
    }
}
