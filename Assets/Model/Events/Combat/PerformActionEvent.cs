using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;

namespace Model.Events.Combat
{
    public class PerformActionEventInfo
    {
        public PerformActionEventInfo(Callback callback = null) { this._callBack = callback; }

        private Callback _callBack;
        public delegate void Callback();

        public CombatEventManager Parent { get; set; }
        public GenericAbility Action { get; set; }
        public TileController Source { get; set; }
        public TileController Target { get; set; }
        public CombatManager CombatManager { get; set; }
    }

    public class PerformActionEvent : CombatEvent
    {
        public GenericCharacterController SourceCharController;
        public GenericCharacterController TargetCharController;

        public PerformActionEventInfo Info {get;set;}

        public PerformActionEvent(PerformActionEventInfo info) :
            base(CombatEventEnum.PerformActionEvent, info.Parent)
        {
            this.Info = info;

            if (!this._parent.GetInteractionLock())
            {
                if (this.Info.Source.Model.Current.GetType() == typeof(GenericCharacterController) &&
                    this.Info.Target.Model.Current.GetType() == typeof(GenericCharacterController))
                {
                    this.SourceCharController = Info.Source.Model.Current as GenericCharacterController;
                    this.TargetCharController = Info.Target.Model.Current as GenericCharacterController;

                    if (Info.CombatManager.TargetsOnSameTeam(this.SourceCharController, this.TargetCharController))
                    {
                        // TODO
                    }
                    else
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
            this.Info.Action.ModData.Reset();
        }
    }
}
