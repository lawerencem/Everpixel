using Controller.Managers;

namespace Model.Events.Combat
{
    public class EndTurnEvent : CombatEvent
    {
        public EndTurnEvent(CombatEventManager parent) :
            base(CombatEventEnum.EndTurn, parent)
        {
            this.RegisterEvent();
        }
    }
}
