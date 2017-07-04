﻿using Assets.Model.Events.Combat;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Combat;

namespace Model.Events.Combat
{
    public class PerformActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public ActionEventController Container { get; set; }

        public PerformActionEvent(CombatEventManager parent, TileController initiatingTile, Callback callback) :
            base(CombatEventEnum.PerformActionEvent, parent)
        {
            this.Container = new ActionEventController(this);

            if (!this._parent.GetInteractionLock())
            {
                this.Container.Target = initiatingTile;
                if (initiatingTile.Model.Current != null && 
                    initiatingTile.Model.Current.GetType().Equals(typeof(GenericCharacterController)))
                {
                    this.Container.TargetCharController = initiatingTile.Model.Current as GenericCharacterController;
                }
                this._callBack = callback;
                this._parent.RegisterEvent(this);
            }
        }

        public void ChildHitDone()
        {
            var done = this.Container.Hits.FindAll(x => x.IsFinished == true);
            if (this.Container.Hits.Count == done.Count)
            {
                this.Container.Action.ModData.Reset();

                if (this._callBack != null)
                    this._callBack();
            }
        }

        public void CastDoneReRegister(GenericCharacterController target)
        {
            this.Container.CastFinished = true;
            if (target != null)
                this.Container.Target = target.CurrentTile;
            this.ProcessActionHits();
        }

        public bool ValidAction()
        {
            return this.Container.Action.IsValidActionEvent(this);
        }

        public void Perform()
        {
            this.ProcessEventStats();
        }

        private void ProcessEventStats()
        {
            double fatigueCost = this.Container.Action.StaminaCost;

            if (this.Container.Source.Model.Armor != null)
                fatigueCost *= this.Container.Source.Model.Armor.FatigueCost;
            if (this.Container.Source.Model.Helm != null)
                fatigueCost *= this.Container.Source.Model.Helm.FatigueCost;
            if (this.Container.Source.Model.LWeapon != null)
                fatigueCost *= this.Container.Source.Model.LWeapon.FatigueCostMod;
            if (this.Container.Source.Model.RWeapon != null)
                fatigueCost *= this.Container.Source.Model.RWeapon.FatigueCostMod;

            if (this.Container.Action.APCost <= this.Container.Source.Model.CurrentAP &&
                fatigueCost <= this.Container.Source.Model.CurrentStamina)
            {
                this.Container.Source.Model.CurrentAP -= this.Container.Action.APCost;
                this.Container.Source.Model.CurrentStamina -= (int)fatigueCost;

                if (this.Container.CastFinished || this.Container.Action.CastTime <= 0)
                {
                    this.ProcessActionHits();
                }
                else
                {
                    var cast = new CastingEvent(this._parent, this);
                }
            }
            else
            {
                // TODO
            }
        }

        private void ProcessActionHits()
        {   
            var hitTargets = this.Container.Action.GetAoETiles(this);
            foreach (var target in hitTargets)
            {
                var hit = new HitInfo(this.Container.Source, target, this.Container.Action, this.ChildHitDone);
                this.Container.Hits.Add(hit);
            }
            this.Container.PerformAction();
        }
    }
}
