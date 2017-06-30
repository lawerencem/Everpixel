using Controller.Managers;
using Controller.Map;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class TileHoverDecoEvent : CombatEvent
    {
        public List<TileController> AoETiles { get; set; }
        public TileController Tile { get; set; }

        public TileHoverDecoEvent(CombatEventManager parent, TileController t, List<TileController> aoe) 
            : base(CombatEventEnum.TileHoverDeco, parent)
        {
            this.AoETiles = aoe;
            this.Tile = t;
            this.RegisterEvent();
        }
    }
}
