using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneArgsCont
    {
        public int Dur { get; set; }
        public CChar Caster { get; set; }
        public GameObject Handle { get; set; }
        public CTile Tile { get; set; }
    }
}