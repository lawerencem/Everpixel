using Controller.Managers;
using Generics;

namespace Model.Events.Combat
{
    public class CombatEvent
    {
        protected CombatEventManager _parent;
        protected ECombatEv _type;
        public ECombatEv Type { get { return this._type; } }

        public CombatEvent(ECombatEv t, CombatEventManager parent)
        {
            this._parent = parent;
            this._type = t;
        }

        protected void RegisterEvent() { this._parent.RegisterEvent(this); }
    }
}
