using Controller.Managers;
using Controller.Map;

namespace Model.Events.Combat
{
    public class HexSelectedForMoveEvent : CombatEvent
    {
        public TileController Selected { get; set; }

        public HexSelectedForMoveEvent(TileController t, CombatEventManager parent) : 
            base(CombatEventEnum.HexSelectedForMove, parent)
        {
            this.Selected = t;
            this.RegisterEvent();
        }
    }
}
