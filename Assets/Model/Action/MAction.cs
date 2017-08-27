using Assets.Controller.GUI;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;

namespace Assets.Model.Action
{
    public class MAction : AAction
    {
        public MAbility ActiveAbility;

        public MAction(ActionData d) : base(d) {}

        public void CalculatAbility()
        {
            if (this.Data.Initialized())
            {
                this.InitAbility();
                this.InitHits();
                this.ProcessAction();
                this.DisplayAction();
            }   
        }

        private void InitAbility()
        {
            GUIManager.Instance.SetGUILocked(true);
            GUIManager.Instance.SetInteractionLocked(true);
            this.ActiveAbility = AbilityTable.Instance.Table[this._data.Ability];
        }

        private void InitHits()
        {
            var args = new AbilityArgs();
            args.AoE = (int)this.ActiveAbility.Data.AoE;
            args.LWeapon = this._data.LWeapon;
            args.Range = this.ActiveAbility.Data.Range;
            args.Source = this._data.Source;
            args.Target = this._data.Target;
            this._data.Hits = this.ActiveAbility.GetHits(args);
        }

        private void ProcessAction()
        {

        }

        private void DisplayAction()
        {
            VCombatController.Instance.DisplayNewAction(this);
        }

        public void Done()
        {
            GUIManager.Instance.SetGUILocked(false);
            GUIManager.Instance.SetInteractionLocked(false);
        }
    }
}
