using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DisplayHitStatsEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public Hit Hit { get; set; }

        public DisplayHitStatsEvent(CombatEventManager parent, Hit hit, Callback callback) 
            : base(ECombatEv.DisplayHitStats, parent)
        {
            this._parent.LockInteraction();
            this.Hit = hit;
            this._callBack = callback;
            this.RegisterEvent();
        }

        public void Done()
        {
            if (this._callBack != null)
                this._callBack();
        }
    }
}
