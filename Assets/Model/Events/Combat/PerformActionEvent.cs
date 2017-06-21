using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Combat;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class PerformActionEventInfo
    {
        public bool CastFinished = false;

        public PerformActionEventInfo()
        {
            this.Hits = new List<HitInfo>();
        }

        public CombatEventManager Parent { get; set; }
        public GenericAbility Action { get; set; }
        public List<HitInfo> Hits { get; set; }
        public GenericCharacterController Source { get; set; }
        public GenericCharacterController TargetCharController { get; set; }
        public TileController Target { get; set; }
        public CombatManager CombatManager { get; set; }
    }

    public class PerformActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public PerformActionEventInfo Info {get;set;}

        public PerformActionEvent(CombatEventManager parent, TileController initiatingTile, Callback callback) :
            base(CombatEventEnum.PerformActionEvent, parent)
        {
            this.Info = new PerformActionEventInfo();

            if (!this._parent.GetInteractionLock())
            {
                this.Info.Target = initiatingTile;
                if (initiatingTile.Model.Current != null && 
                    initiatingTile.Model.Current.GetType().Equals(typeof(GenericCharacterController)))
                {
                    this.Info.TargetCharController = initiatingTile.Model.Current as GenericCharacterController;
                }
                this._callBack = callback;
                this._parent.RegisterEvent(this);
            }
        }

        public void ChildHitDone()
        {
            var done = this.Info.Hits.FindAll(x => x.IsFinished == true);
            if (this.Info.Hits.Count == done.Count)
            {
                this.Info.Action.ModData.Reset();

                if (this._callBack != null)
                    this._callBack();
            }
        }

        public void CastDoneReRegister()
        {
            this.Info.CastFinished = true;
            var hit = new HitInfo(this.Info.Source, this.Info.Target, this.Info.Action, this.ChildHitDone);
            this.Info.Hits.Add(hit);
            this.Info.Action.ProcessAbility(hit);
        }

        public bool ValidAction()
        {
            return this.Info.Action.IsValidActionEvent(this);
        }

        public void Perform()
        {
            this.ProcessEventStats();
        }

        private void ProcessEventStats()
        {
            double fatigueCost = this.Info.Action.StaminaCost;

            if (this.Info.Source.Model.Armor != null)
                fatigueCost *= this.Info.Source.Model.Armor.FatigueCost;
            if (this.Info.Source.Model.Helm != null)
                fatigueCost *= this.Info.Source.Model.Helm.FatigueCost;
            if (this.Info.Source.Model.LWeapon != null)
                fatigueCost *= this.Info.Source.Model.LWeapon.FatigueCostMod;
            if (this.Info.Source.Model.RWeapon != null)
                fatigueCost *= this.Info.Source.Model.RWeapon.FatigueCostMod;

            if (this.Info.Action.APCost <= this.Info.Source.Model.CurrentAP &&
                fatigueCost <= this.Info.Source.Model.CurrentStamina)
            {
                if (this.Info.CastFinished || this.Info.Action.CastTime <= 0)
                {
                    this.Info.Source.Model.CurrentAP -= this.Info.Action.APCost;
                    this.Info.Source.Model.CurrentStamina -= (int)fatigueCost;

                    var hit = new HitInfo(this.Info.Source, this.Info.Target, this.Info.Action, this.ChildHitDone);
                    this.Info.Hits.Add(hit);
                    this.Info.Action.ProcessAbility(hit);
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
    }
}
