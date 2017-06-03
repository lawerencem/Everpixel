using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DisplayHitStatsEvent : CombatEvent
    {
        private Callback _callBack;

        public HitInfo Hit { get; set; }

        public delegate void Callback();

        public DisplayHitStatsEvent(CombatEventManager parent, HitInfo hit) 
            : base(CombatEventEnum.DisplayHitStats, parent)
        {
            this._parent.LockInteraction();
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
