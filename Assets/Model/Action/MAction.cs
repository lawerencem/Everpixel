using Assets.Controller.Character;
using Assets.Model.Ability;

namespace Assets.Model.Action
{
    public class MAction : AAction
    {
        private MAbility _ability;

        public MAction(ActionData d) : base(d) {}

        public void CalculatAbility()
        {
            if (this.Data.Initialized())
            {
                this.InitHits();
            }
                
        }

        private void InitAbility()
        {
            this._ability = AbilityTable.Instance.Table[this._data.Ability];
        }

        private void InitHits()
        {
            var args = new AbilityArgs();
            args.LWeapon = this._data.LWeapon;
            args.Range = this._ability.Data.Range;
            args.Source = this._data.Source;
            args.Target = this._data.Target;
            var targetTiles = this._ability.GetTargetTiles(args);
        }
    }
}
