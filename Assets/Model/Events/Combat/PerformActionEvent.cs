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
        public TileController Source { get; set; }
        public TileController Target { get; set; }
        public CombatManager CombatManager { get; set; }
    }

    public class PerformActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public GenericCharacterController SourceCharController;
        public GenericCharacterController TargetCharController;

        public PerformActionEventInfo Info {get;set;}

        public PerformActionEvent(PerformActionEventInfo info, Callback callback) :
            base(CombatEventEnum.PerformActionEvent, info.Parent)
        {
            this._callBack = callback;
            this.Info = info;

            if (!this._parent.GetInteractionLock())
            {
                this.SourceCharController = Info.Source.Model.Current as GenericCharacterController;

                // TODO: NEed to clean this up a lot...
                if (Info.Target.Model.Current == null)
                    this.ProcessEventStats();
                else
                    this.ProcessCharacterSelected();
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
            this.RegisterEvent();
        }

        private void ProcessCharacterSelected()
        {
            if (this.Info.Source.Model.Current.GetType() == typeof(GenericCharacterController) &&
                this.Info.Target.Model.Current.GetType() == typeof(GenericCharacterController))
            {
                this.TargetCharController = Info.Target.Model.Current as GenericCharacterController;

                if (Info.CombatManager.TargetsOnSameTeam(this.SourceCharController, this.TargetCharController))
                {
                    // TODO
                }
                else
                {
                    this.ProcessEventStats();
                }
            }
        }

        private void ProcessEventStats()
        {
            double fatigueCost = this.Info.Action.StaminaCost;

            if (this.SourceCharController.Model.Armor != null)
                fatigueCost *= this.SourceCharController.Model.Armor.FatigueCost;
            if (this.SourceCharController.Model.Helm != null)
                fatigueCost *= this.SourceCharController.Model.Helm.FatigueCost;
            if (this.SourceCharController.Model.LWeapon != null)
                fatigueCost *= this.SourceCharController.Model.LWeapon.FatigueCostMod;
            if (this.SourceCharController.Model.RWeapon != null)
                fatigueCost *= this.SourceCharController.Model.RWeapon.FatigueCostMod;

            if (this.Info.Action.APCost <= this.SourceCharController.Model.CurrentAP &&
                fatigueCost <= this.SourceCharController.Model.CurrentStamina)
            {
                this.SourceCharController.Model.CurrentAP -= this.Info.Action.APCost;
                this.SourceCharController.Model.CurrentStamina -= (int)fatigueCost;
                this.RegisterEvent();
            }
        }
    }
}
