using Assets.Model.Events.Combat;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Combat;

namespace Model.Events.Combat
{
    public class PerformActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public ActionEventController Container { get; set; }

        public PerformActionEvent(CombatEventManager parent,  TileController initiatingTile, Callback callback) :
            base(CombatEventEnum.PerformAction, parent)
        {
            this.Container = new ActionEventController(this);

            this.Container.Target = initiatingTile;
            var character = initiatingTile.Model.Current;

            if (character != null && character.GetType().Equals(typeof(CharController)))
                this.Container.TargetCharController = initiatingTile.Model.Current as CharController;

            if (!this._parent.GetInteractionLock())
            {
                this._callBack = callback;
                this._parent.RegisterEvent(this);
            }
        }

        public void ChildHitDone()
        {
            var done = this.Container.Hits.FindAll(x => x.IsFinished == true);
            if (this.Container.Hits.Count == done.Count)
            {
                if (this._callBack != null)
                    this._callBack();
            }
        }

        public void CastDoneReRegister(CharController target)
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

            if (this.Container.Action.GetAPCost() <= this.Container.Source.Model.GetCurrentAP() &&
                fatigueCost <= this.Container.Source.Model.GetCurrentStamina())
            {
                this.Container.Source.Model.ModifyStamina((int)fatigueCost, false);
                var curAP = this.Container.Source.Model.GetCurrentAP();
                curAP -= (int)this.Container.Action.GetAPCost();
                this.Container.Source.Model.SetCurrentAP(curAP);

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
            var hitTargets = this.Container.Action.GetAoETiles(
                this.Container.Target, 
                this.Container.Source.CurrentTile, 
                this.Container.Action.Range);
            
            if (this.Container.Action.CastType == CastTypeEnum.Raycast)
                this.Container.Target = hitTargets[hitTargets.Count - 1];

            foreach (var target in hitTargets)
            {
                var hit = new Hit(this.Container.Source, target, this.Container.Action, this.ChildHitDone);
                this.Container.Hits.Add(hit);
            }
            this.Container.PerformAction();
        }
    }
}
