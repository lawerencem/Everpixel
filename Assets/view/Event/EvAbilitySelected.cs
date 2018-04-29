using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;

namespace Assets.View.Event
{
    public class EvAbilitySelectedData
    {
        public EAbility Ability { get; set; }
        public bool LWeapon { get; set; }
        public CChar Source { get; set; }
        public bool WpnAbility { get; set; }
    }

    public class EvAbilitySelected : MGuiEv
    {
        private EvAbilitySelectedData _data;

        public EvAbilitySelected() : base(EGuiEv.TileClick) { }
        public EvAbilitySelected(EvAbilitySelectedData d) : base(EGuiEv.AbilitySelected) { this._data = d; }

        public void SetData(EvAbilitySelectedData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsValid())
            {
                var ability = AbilityTable.Instance.Table[this._data.Ability];
                var args = new AbilityArgs();
                args.AoE = (int)ability.Data.AoE;
                args.LWeapon = this._data.LWeapon;
                args.Range = ability.Data.Range;
                args.Source = this._data.Source;
                args.WpnAbility = this._data.WpnAbility;
                var tiles = ability.GetTargetableTiles(args);
                VMapCombatController.Instance.DecoratePotentialTargetTiles(tiles);
                CombatManager.Instance.SetPotentialTgtTiles(tiles);

                var data = new CurrentlyActingData();
                data.Ability = this._data.Ability;
                data.CurrentlyActing = CombatManager.Instance.GetCurrentlyActing();
                data.LWeapon = this._data.LWeapon;

                if (this._data.LWeapon)
                    data.CurrentWeapon = data.CurrentlyActing.Proxy.GetLWeapon();
                else
                    data.CurrentWeapon = data.CurrentlyActing.Proxy.GetRWeapon();

                data.IsWpnAbility = this._data.WpnAbility;
                CombatManager.Instance.SetCurrentData(data);
            }
        }

        private bool IsValid()
        {
            if (this._data.Ability != EAbility.None &&
                this._data.Source != null &&
                AbilityTable.Instance.Table.ContainsKey(this._data.Ability))
            {
                return true;
            }
            return false;
        }
    }
}
