using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic;

namespace Assets.Model.Ability
{
    public abstract class AAbility
    {
        protected AbilityData _data;
        protected AbilityLogic _logic;
        protected bool _wpnAbility;

        protected EAbility _type;
        public EAbility Type { get { return this._type; } }
        public bool WpnAbility { get { return this._wpnAbility; } }

        public AbilityData Data { get { return this._data; } }

        public void SetData(AbilityData d) { this._data = d; }

        public AAbility(EAbility type)
        {
            this._data = new AbilityData();
            this._logic = new AbilityLogic();
            this._type = type;
        }
    }
}
