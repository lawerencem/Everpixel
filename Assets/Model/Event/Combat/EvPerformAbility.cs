using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;

namespace Assets.Model.Event.Combat
{
    public class EvPerformAbilityData
    {
        public EAbility Ability { get; set; }
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public CChar Source { get; set; }
        public CTile Target { get; set; }
        public bool WpnAbility { get; set; }
    }

    public class EvPerformAbility : MEvCombat
    {
        private EvPerformAbilityData _data;

        public EvPerformAbility() : base(ECombatEv.PerformAbility) { }
        public EvPerformAbility(EvPerformAbilityData d) : base(ECombatEv.PerformAbility) { this._data = d; }

        public EvPerformAbilityData GetData() { return this._data; }
        public void SetData(EvPerformAbilityData d) { this._data = d; }

        public override void TryProcess()
        {
            base.TryProcess();
            var data = new ActionData();
            data.Ability = this._data.Ability;
            data.LWeapon = this._data.LWeapon;
            data.ParentWeapon = this._data.ParentWeapon;
            data.Source = this._data.Source;
            data.Target = this._data.Target;
            data.WpnAbility = this._data.WpnAbility;
            var action = new MAction(data);
            foreach (var callback in this._callbacks)
                action.AddCallback(callback);
            action.TryProcess();
        }
    }
}
