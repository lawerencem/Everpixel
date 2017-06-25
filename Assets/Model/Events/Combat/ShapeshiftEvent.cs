using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;

namespace Model.Events.Combat
{
    public class ShapeshiftEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public ShapeshiftEvent(CombatEventManager parent, HitInfo hit) :
            base(CombatEventEnum.Shapeshift, parent)
        {
            this.Hit = hit;
            this.RegisterEvent();
        }
    }
}
