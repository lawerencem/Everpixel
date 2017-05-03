using Controller.Characters;
using Controller.Managers;
using Model.Map;

namespace Model.Events.Combat
{
    public class PathTraversedEvent : CombatEvent
    {
        public GenericCharacterController Character { get; set; }

        public PathTraversedEvent(CombatEventManager parent, GenericCharacterController c) :
            base(CombatEventEnum.PathTraversed, parent)
        {
            this.Character = c;
            this.RegisterEvent();
        }
    }
}
