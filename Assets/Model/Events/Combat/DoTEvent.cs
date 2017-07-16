using Controller.Characters;
using Controller.Managers;
using Model.OverTimeEffects;

namespace Model.Events.Combat
{
    public class DoTEvent : CombatEvent
    {
        public GenericDoT DoT { get; set; }
        public GenericCharacterController ToDoT { get; set; }

        public DoTEvent(CombatEventManager parent, GenericDoT dot, GenericCharacterController toDot) :
            base(CombatEventEnum.Shield, parent)
        {
            this.DoT = dot;
            this.ToDoT = toDot;
            this.ToDoT.Model.AddDoT(dot);
            this.RegisterEvent();
        }
    }
}
