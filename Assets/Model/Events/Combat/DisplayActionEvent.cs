using Assets.Model.Events.Combat;
using Controller.Managers;
using Model.Combat;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class DisplayActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public ActionEventController EventController { get; set; }
        public List<Hit> FatalityHits;

        public DisplayActionEvent(CombatEventManager parent, ActionEventController e, Callback callback = null)
            : base(CombatEventEnum.DisplayAction, parent)
        {
            this._parent.LockInteraction();
            this.EventController = e;
            this._callBack = callback;
            this.FatalityHits = new List<Hit>();
            this.RegisterEvent();
        }

        public void AttackFXDone()
        {
            foreach(var hit in this.EventController.Hits)
            {
                if (!hit.FXProcessed)
                {
                    var e = new DisplayHitStatsEvent(CombatEventManager.Instance, hit, hit.Done);
                }
            }
        }

        public void Done()
        {
            if (this._callBack != null)
                this._callBack();
        }
    }
}
