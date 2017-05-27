using Assets.Generics;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using System.Collections.Generic;

namespace View.Events
{
    public class TileHoverEvent : GUIEvent
    {
        public TileController Tile { get; set; }

        public TileHoverEvent(GUIEventManager parent, TileController tile) :
            base(GUIEventEnum.TileHover, parent)
        {
            this.Tile = tile;
            this._parent.RegisterEvent(this);
        }
    }
}
