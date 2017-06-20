using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;

namespace Model.Events.Combat
{
    public class SummonEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public SummonEvent(CombatEventManager parent, HitInfo hit) :
            base(CombatEventEnum.Summon, parent)
        {
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
