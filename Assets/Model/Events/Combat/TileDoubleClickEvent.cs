using Controller.Managers;
using Controller.Map;

namespace Model.Events.Combat
{
    public class TileDoubleClickEvent : CombatEvent
    {
        public TileController Tile { get; set; }

        public TileDoubleClickEvent(CombatEventManager parent, TileController t) : base(CombatEventEnum.TileDoubleClick, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.Tile = t;
                this.RegisterEvent();
            }
        }
    }
}
