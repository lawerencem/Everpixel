using Controller.Characters;
using Controller.Managers;
using Model.Map;

namespace Model.Events.Combat
{
    public class TraversePathEvent : CombatEvent
    {
        public CharController Character { get; set; }
        public Path Path { get; set; }

        public TraversePathEvent(CombatEventManager parent, CharController c, Path p) : 
            base(CombatEventEnum.TraversePath, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this._parent.LockInteraction();
                this.Character = c;
                this.Path = p;
                this.RegisterEvent();
            }
        }
    }
}
