using Controller.Characters;
using Controller.Managers;
using Model.OverTimeEffects;

namespace Model.Events.Combat
{
    public class DoTEvent : CombatEvent
    {
        public GenericDoT DoT { get; set; }
        public CharController ToDoT { get; set; }

        public DoTEvent(CombatEventManager parent, GenericDoT dot, CharController toDot) :
            base(CombatEventEnum.DoT, parent)
        {
            this.DoT = dot;
            this.ToDoT = toDot;
            this.ToDoT.Model.AddDoT(dot);
            this.RegisterEvent();
        }
    }
}
