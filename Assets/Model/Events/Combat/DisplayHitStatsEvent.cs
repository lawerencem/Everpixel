using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DisplayHitStatsEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public DisplayHitStatsEvent(CombatEventManager parent, HitInfo hit) 
            : base(CombatEventEnum.DisplayHitStats, parent)
        {
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
