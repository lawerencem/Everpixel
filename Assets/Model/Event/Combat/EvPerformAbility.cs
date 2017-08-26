using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;

namespace Assets.Model.Event.Combat
{
    public class EvPerformAbilityData
    {
        public EAbility Ability { get; set; }
        public bool LWeapon { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }
    }

    public class EvPerformAbility : MEvCombat
    {
        private EvPerformAbilityData _data;

        public EvPerformAbility() : base(ECombatEv.PerformAbility) { }
        public EvPerformAbility(EvPerformAbilityData d) : base(ECombatEv.PerformAbility) { this._data = d; }

        public void SetData(EvPerformAbilityData d) { this._data = d; }

        public override void TryProcess()
        {
            base.TryProcess();
            var data = new ActionData();
            data.Ability = this._data.Ability;
            data.LWeapon = this._data.LWeapon;
            data.Source = this._data.Source;
            data.Target = this._data.Target;
            var action = new MAction(data);
            action.CalculatAbility();
        }
    }
}
