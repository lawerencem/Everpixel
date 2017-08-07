using Assets.Controller.Character;
using Controller.Map;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneArgsCont
    {
        public int Dur { get; set; }
        public CharController Caster { get; set; }
        public GameObject Handle { get; set; }
        public TileController Tile { get; set; }
    }
}