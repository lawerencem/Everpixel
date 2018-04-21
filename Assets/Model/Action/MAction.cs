using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Character.Enum;
using Assets.Model.Event.Combat;
using Assets.Model.Injury.Calculator;

namespace Assets.Model.Action
{
    public class MAction : AAction
    {
        private InjuryCalculator _injuryCalc;

        public MAbility ActiveAbility;

        public MAction(ActionData d) : base(d)
        {
            this._injuryCalc = new InjuryCalculator();
        }

        public void TryPredict()
        {
            if (this.Data.Initialized())
            {
                this.InitPredictAbility();
                this.InitPredictHits();
            }
        }

        public void TryProcessPostPredict()
        {
            this.ProcessAction();
            this.DisplayAction();
        }

        public void TryProcess()
        {
            if (this.Data.Initialized())
            {
                this.InitProcessAbility();
                this.InitProcessHits();
                this.ProcessAction();
                this.DisplayAction();
            }   
        }

        private void InitPredictAbility()
        {
            this.ActiveAbility = AbilityTable.Instance.Table[this._data.Ability];
            this.ActiveAbility.Data.ParentAction = this;
            if (this.Data.ParentWeapon != null)
                this.ActiveAbility.Data.ParentWeapon = this.Data.ParentWeapon.Model;
        }

        private void InitProcessAbility()
        {
            GUIManager.Instance.SetGUILocked(true);
            GUIManager.Instance.SetInteractionLocked(true);
            this.ActiveAbility = AbilityTable.Instance.Table[this._data.Ability];
            this.ActiveAbility.Data.ParentAction = this;
            if (this.Data.ParentWeapon != null)
                this.ActiveAbility.Data.ParentWeapon = this.Data.ParentWeapon.Model;
        }

        private void InitPredictHits()
        {
            var args = new AbilityArgs();
            args.AoE = (int)this.ActiveAbility.Data.AoE;
            args.LWeapon = this._data.LWeapon;
            args.Range = this.ActiveAbility.Data.Range;
            args.Source = this._data.Source;
            args.Target = this._data.Target;
            args.WpnAbility = this._data.WpnAbility;
            this._data.Hits = this.ActiveAbility.GetHits(args);
            foreach (var hit in this._data.Hits)
            {
                this.ActiveAbility.Predict(hit);
            }
        }

        private void InitProcessHits()
        {
            var args = new AbilityArgs();
            args.AoE = (int)this.ActiveAbility.Data.AoE;
            args.LWeapon = this._data.LWeapon;
            args.Range = this.ActiveAbility.Data.Range;
            args.Source = this._data.Source;
            args.Target = this._data.Target;
            args.WpnAbility = this._data.WpnAbility;
            this._data.Hits = this.ActiveAbility.GetHits(args);
            foreach(var hit in this._data.Hits)
            {
                hit.AddCallback(this.TryDone);
                this.ActiveAbility.Process(hit);
            }
        }

        private void ProcessAction()
        {
            foreach (var hit in this._data.Hits)
                this._injuryCalc.ProcessHitInjuries(hit);
            var staminaCalc = new StaminaCalculator();
            var cost = staminaCalc.Process(this);
            this.Data.Source.Proxy.ModifyPoints(ESecondaryStat.Stamina, cost, false);
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
            if (done && !this._completed)
            {
                this._completed = true;
                GUIManager.Instance.SetGUILocked(false);
                GUIManager.Instance.SetInteractionLocked(false);
                this.ProcessHitsData();
                this.DoCallbacks();
            }
        }

        private void ProcessHitsData()
        {
            foreach(var hit in this._data.Hits)
            {
                if (hit.Data.Target.Current != null)
                {
                    if (hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                    {
                        var target = hit.Data.Target.Current as CChar;
                        var data = new EvModHPData();
                        data.Dmg = hit.Data.Dmg;
                        data.Flags = hit.Data.Flags;
                        data.Hit = hit;
                        data.IsFatality = hit.Data.IsFatality;
                        data.IsHeal = hit.Data.IsHeal;
                        data.Target = target;
                        var e = new EvModHP(data);
                        e.TryProcess();
                    }
                }
            }
        }
    }
}
