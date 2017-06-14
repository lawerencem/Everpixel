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
            if (!this._parent.GetInteractionLock() && !this._parent.GetGUILock())
            {
                this.Selected = t;
                this.RegisterEvent();
            }
        }
    }
}