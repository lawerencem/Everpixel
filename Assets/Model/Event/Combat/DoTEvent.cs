using Controller.Characters;
using Controller.Managers;
using Model.OverTimeEffects;

namespace Model.Events.Combat
{
    public class DoTEvent : CombatEvent
    {
        public MDoT DoT { get; set; }
        public CharController ToDoT { get; set; }

        public DoTEvent(CombatEventManager parent, MDoT dot, CharController toDot) :
            base(ECombatEv.DoT, parent)
        {
            this.DoT = dot;
            this.ToDoT = toDot;
            this.ToDoT.Model.AddDoT(dot);
            this.RegisterEvent();
        }
    }
}
