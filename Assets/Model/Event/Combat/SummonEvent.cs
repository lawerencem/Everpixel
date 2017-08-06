using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;

namespace Model.Events.Combat
{
    public class SummonEvent : CombatEvent
    {
        public Hit Hit { get; set; }

        public SummonEvent(CombatEventManager parent, Hit hit) :
            base(ECombatEv.Summon, parent)
        {
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
