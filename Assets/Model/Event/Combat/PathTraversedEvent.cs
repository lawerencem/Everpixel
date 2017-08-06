using Controller.Characters;
using Controller.Managers;
using Model.Map;

namespace Model.Events.Combat
{
    public class PathTraversedEvent : CombatEvent
    {
        public CharController Character { get; set; }

        public PathTraversedEvent(CombatEventManager parent, CharController c) :
            base(ECombatEv.PathTraversed, parent)
        {
            this.Character = c;
            this.RegisterEvent();
            this._parent.UnlockInteraction();
        }
    }
}
