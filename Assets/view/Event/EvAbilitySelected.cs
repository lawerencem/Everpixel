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
        public CharController Source { get; set; }
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
            CombatManager.Instance.SetCurrentAbility(this._data.Ability);
            if (this.IsValid())
            {
                var ability = AbilityTable.Instance.Table[this._data.Ability];
                var args = new AbilityArgContainer();
                args.Range = ability.Params.Range;
                args.Source = this._data.Source;
                var tiles = ability.GetTargetTiles(args);
                VMapController.Instance.DecoratePotentialTargetTiles(tiles);
                CombatManager.Instance.SetTgtTiles(tiles);
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