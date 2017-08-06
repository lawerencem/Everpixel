using Controller.Characters;
using Controller.Managers;
using Generics;

namespace Model.Events.Combat
{
    public class TakingActionEvent : CombatEvent
    {
        public CharController Controller { get; set; }

        public TakingActionEvent(CombatEventManager p, CharController c) : 
            base(ECombatEv.TakingAction, p)
        {
            this.Controller = c;

            this._parent.RegisterEvent(this);
        }
    }
}
