using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Model.Injury.Calculator;
using Assets.Template.Event;
using Assets.Template.Other;
using System;
using Template.Script;

namespace Assets.Model.Action
{
    public class MAction : AAction, IChildEvent
    {
        private bool _castingInitialized = false;
        private int _castingTurnsRemaining = 0;

        public MAbility ActiveAbility;

        public MAction(ActionData data) : base(data)
        {
            this.ValidateData();
        }

        public void DecrementCastingTurnsRemaining()
        {
            this._castingTurnsRemaining--;
        }

        public void DisplayAction()
        {
            VCombatController.Instance.DisplayNewAction(this);
        }

        public int GetCastingTurnsRemaining()
        {
            return this._castingTurnsRemaining;
        }

        public void TryDone(object o)
        {
            bool done = true;
            foreach (var hit in this._data.Hits)
            {
                if (!hit.Done)
                    done = false;
            }
            if (done && !this._completed)
            {
                this.ProcessHitsData();
                if (this._castingInitialized)
                    this.HandleDoneCasting();
                this.HandleHitCounters();
                this.CallbackHandler(null);
            }
        }

        public void TryPredict()
        {
            if (this.Data.Initialized())
            {
                this.InitPredictAbility();
                this.InitPredictHits();
            }
        }

        public void TryProcess()
        {
            if (this.Data.Initialized())
            {
                this.InitProcessAbility();
                if (this._castingTurnsRemaining <= 0)
                {
                    this.InitProcessHits();
                    this.ProcessAction();
                    this.DisplayAction();
                }
                else
                    this.HandleCasting();
            }   
        }

        public void TryProcessNoDisplay()
        {
            if (this.Data.Initialized())
            {
                this.InitProcessAbility();
                if (this._castingTurnsRemaining <= 0)
                {
                    this.InitProcessHits();
                    this.ProcessAction();
                }
                else
                    this.HandleCasting();
            }
        }

        private void HandleCasting()
        {
            FActionStatus.SetCastingTrue(this.Data.Source.Proxy.GetActionFlags());
            GUIManager.Instance.SetGUILocked(false);
            GUIManager.Instance.SetInteractionLocked(false);
            CombatManager.Instance.SetCurrentAbilityNone();
            var pair = new Pair<CChar, MAction>(this.Data.Source, this);
            CombatManager.Instance.AddCurrentlyCasting(pair);
            this.HandleCastingJolt();
            var e = new EvEndTurn();
            e.TryProcess();
        }

        private void HandleCastingJolt()
        {
            var jolt = this.Data.Source.GameHandle.GetComponent<SIntervalJoltScript>();
            if (jolt == null)
            {
                // TODO: Const out
                var data = new SIntervalJoltScriptData();
                data.Perpetual = true;
                data.Speed = 10f;
                data.TimeInterval = 1f;
                data.ToJolt = this.Data.Source.GameHandle;
                data.X = 0.1f;
                data.Y = 0.1f;
                jolt = this.Data.Source.GameHandle.AddComponent<SIntervalJoltScript>();
                jolt.Init(data);
            }
        }

        private void HandleDoneCasting()
        {
            var jolt = this.Data.Source.GameHandle.GetComponent<SIntervalJoltScript>();
            jolt.Done();
            FActionStatus.SetCastingFalse(this.Data.Source.Proxy.GetActionFlags());
        }

        private void HandleHitCounters()
        {
            foreach (var hit in this._data.Hits)
            {
                var target = hit.Data.Target.Current;
                if (target != null && target.GetType().Equals(typeof(CChar)))
                {
                    var tgt = target as CChar;
                    if (FActionStatus.HasFlag(tgt.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Riposting))
                    {
                        if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block) ||
                            FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) ||
                            FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                        {
                            this.HandleHitCounterHelper(hit, tgt);
                        }
                    }
                }
            }
        }

        private void HandleHitCounterHelper(MHit hit, CChar tgt)
        {
            var data = new ActionData();
            data.Ability = EAbility.Attack_Of_Opportunity;
            bool proceed = false;
            if (tgt.Proxy.GetLWeapon() != null && !tgt.Proxy.GetLWeapon().IsTypeOfShield())
            {
                data.LWeapon = true;
                data.ParentWeapon = tgt.Proxy.GetLWeapon();
                proceed = true;
            }
            else if (tgt.Proxy.GetRWeapon() != null && !tgt.Proxy.GetRWeapon().IsTypeOfShield())
            {
                data.LWeapon = false;
                data.ParentWeapon = tgt.Proxy.GetRWeapon();
                proceed = true;
            }
            if (proceed)
            {
                data.Source = tgt;
                data.Target = hit.Data.Source.Tile;
                data.WpnAbility = true;
                var action = new MAction(data);
                action.TryProcess();
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
            if (this.ActiveAbility.Data.CastDuration > 0 && this._castingInitialized == false)
            {
                this._castingTurnsRemaining = this.ActiveAbility.Data.CastDuration;
                this._castingInitialized = true;
            }
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
            var calc = new InjuryCalculator();
            foreach (var hit in this._data.Hits)
                calc.ProcessHitInjuries(hit);
            var staminaCalc = new StaminaCalculator();
            var cost = staminaCalc.Process(this);
            this.Data.Source.Proxy.ModifyPoints(ESecondaryStat.Stamina, cost, false);
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

        private void ValidateData()
        {
            if (this._data.Ability == EAbility.None)
                throw new Exception("Error: Ability may not be null for new action.");
            else if (this._data.Source == null)
                throw new Exception("Error: Source of action may not be null.");
        }
    }
}
