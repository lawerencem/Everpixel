using Controller.Characters;
using Controller.Managers;
using Generics;

namespace Model.Events.Combat
{
    public class TakingActionEvent : CombatEvent
    {
        public GenericCharacterController Controller { get; set; }

        public TakingActionEvent(CombatEventManager p, GenericCharacterController c) : 
            base(CombatEventEnum.TakingAction, p)
        {
            this.Controller = c;
            this._parent.RegisterEvent(this);
        }
    }
}
