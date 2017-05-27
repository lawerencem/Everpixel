using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class TileHoverDecoEvent : CombatEvent
    {
        public TileController Tile { get; set; }

        public TileHoverDecoEvent(CombatEventManager parent, TileController t) : base(CombatEventEnum.TileHoverDeco, parent)
        {
            this.Tile = t;
            this.RegisterEvent();
        }
    }
}
