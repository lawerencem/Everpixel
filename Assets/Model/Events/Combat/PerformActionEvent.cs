using Assets.Model.Events.Combat;
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

        public ActionEventController ActionContainer { get; set; }

        public PerformActionEvent(CombatEventManager parent, TileController initiatingTile, Callback callback) :
            base(CombatEventEnum.PerformActionEvent, parent)
        {
            this.ActionContainer = new ActionEventController(this);

            if (!this._parent.GetInteractionLock())
            {
                this.ActionContainer.Target = initiatingTile;
                if (initiatingTile.Model.Current != null && 
                    initiatingTile.Model.Current.GetType().Equals(typeof(GenericCharacterController)))
                {
                    this.ActionContainer.TargetCharController = initiatingTile.Model.Current as GenericCharacterController;
                }
                this._callBack = callback;
                this._parent.RegisterEvent(this);
            }
        }

        public void ChildHitDone()
        {
            var done = this.ActionContainer.Hits.FindAll(x => x.IsFinished == true);
            if (this.ActionContainer.Hits.Count == done.Count)
            {
                this.ActionContainer.Action.ModData.Reset();

                if (this._callBack != null)
                    this._callBack();
            }
        }

        public void CastDoneReRegister(GenericCharacterController target)
        {
            this.ActionContainer.CastFinished = true;
            this.ActionContainer.Target = target.CurrentTile;
            this.ProcessActionHits();
        }

        public bool ValidAction()
        {
            return this.ActionContainer.Action.IsValidActionEvent(this);
        }

        public void Perform()
        {
            this.ProcessEventStats();
        }

        private void ProcessEventStats()
        {
            double fatigueCost = this.ActionContainer.Action.StaminaCost;

            if (this.ActionContainer.Source.Model.Armor != null)
                fatigueCost *= this.ActionContainer.Source.Model.Armor.FatigueCost;
            if (this.ActionContainer.Source.Model.Helm != null)
                fatigueCost *= this.ActionContainer.Source.Model.Helm.FatigueCost;
            if (this.ActionContainer.Source.Model.LWeapon != null)
                fatigueCost *= this.ActionContainer.Source.Model.LWeapon.FatigueCostMod;
            if (this.ActionContainer.Source.Model.RWeapon != null)
                fatigueCost *= this.ActionContainer.Source.Model.RWeapon.FatigueCostMod;

            if (this.ActionContainer.Action.APCost <= this.ActionContainer.Source.Model.CurrentAP &&
                fatigueCost <= this.ActionContainer.Source.Model.CurrentStamina)
            {
                this.ActionContainer.Source.Model.CurrentAP -= this.ActionContainer.Action.APCost;
                this.ActionContainer.Source.Model.CurrentStamina -= (int)fatigueCost;

                if (this.ActionContainer.CastFinished || this.ActionContainer.Action.CastTime <= 0)
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
            var hitTargets = this.ActionContainer.Action.GetAoETiles(this);
            foreach (var target in hitTargets)
            {
                var hit = new HitInfo(this.ActionContainer.Source, target, this.ActionContainer.Action, this.ChildHitDone);
                this.ActionContainer.Hits.Add(hit);
            }
            this.ActionContainer.PerformAction();
        }
    }
}
