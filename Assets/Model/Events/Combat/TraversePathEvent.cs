using Controller.Characters;
using Controller.Managers;
using Model.Map;

namespace Model.Events.Combat
{
    public class TraversePathEvent : CombatEvent
    {
        public GenericCharacterController Character { get; set; }
        public Path Path { get; set; }

        public TraversePathEvent(CombatEventManager parent, GenericCharacterController c, Path p) : 
            base(CombatEventEnum.TraversePath, parent)
        {
            this.Character = c;
            this.Path = p;
            this.RegisterEvent();
        }
    }
}
