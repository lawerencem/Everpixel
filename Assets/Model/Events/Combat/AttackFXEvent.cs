using Controller.Managers;
using Model.Combat;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class AttackFXEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public List<HitInfo> ChildHits { get; set; }

        public AttackFXEvent(CombatEventManager parent, Callback callback)
            : base(CombatEventEnum.AttackFX, parent)
        {
            this._parent.LockInteraction();
            this.ChildHits = new List<HitInfo>();
            this._callBack = callback;
            this.RegisterEvent();
        }
    }
}