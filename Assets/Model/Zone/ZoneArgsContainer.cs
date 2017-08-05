using Controller.Characters;
using Controller.Map;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneArgsContainer
    {
        public int Dur { get; set; }
        public CharController Caster { get; set; }
        public GameObject Handle { get; set; }
        public TileController Tile { get; set; }
    }
}