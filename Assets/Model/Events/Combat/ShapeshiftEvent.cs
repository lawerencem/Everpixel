using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;

namespace Model.Events.Combat
{
    public class ShapeshiftEvent : CombatEvent
    {
        public Hit Hit { get; set; }

        public ShapeshiftEvent(CombatEventManager parent, Hit hit) :
            base(CombatEventEnum.Shapeshift, parent)
        {
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
