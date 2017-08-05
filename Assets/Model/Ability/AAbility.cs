using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic;

namespace Assets.Model.Ability
{
    public abstract class AAbility
    {
        protected AbilityLogic _logic;
        protected bool _hostile = true;
        protected EnumAbility _type;

        public bool Hostile { get { return this._hostile; } }
        public EnumAbility Type { get { return this._type; } }

        public AbilityParamContainer Params { get; set; }
    }
}
