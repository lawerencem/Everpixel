using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;
using Assets.Model.Event.Combat;

namespace Assets.Model.Action
{
    public class MAction : AAction
    {
        public MAbility ActiveAbility;

        public MAction(ActionData d) : base(d) {}

        public void TryProcess()
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
            foreach(var hit in this._data.Hits)
            {
                hit.AddCallback(this.TryDone);
                this.ActiveAbility.Process(hit);
            }
        }

        private void ProcessAction()
        {

        }

        private void DisplayAction()
        {
            VCombatController.Instance.DisplayNewAction(this);
        }

        public void TryDone(object o)
        {
            bool done = true;
            foreach(var hit in this._data.Hits)
            {
                if (!hit.Done)
                    done = false;
            }
            if (done)
            {
                GUIManager.Instance.SetGUILocked(false);
                GUIManager.Instance.SetInteractionLocked(false);
                this.ProcessHitsData();
            }
        }

        private void ProcessHitsData()
        {
            foreach(var hit in this._data.Hits)
            {
                if (hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var target = hit.Data.Target.Current as CharController;
                    var data = new EvModHPData();
                    data.Dmg = hit.Data.Dmg;
                    data.Flags = hit.Data.Flags;
                    data.IsHeal = hit.Data.IsHeal;
                    data.Target = target;
                    var e = new EvModHP(data);
                    e.TryProcess();
                }
            }
        }
    }
}
