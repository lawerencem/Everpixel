using Controller.Characters;
using Controller.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Map
{
    public class HexTile
    {
        public HexTile()
        {
            this.Adjacent = new List<HexTile>();
            this.Cost = 3;
            this.Height = 1;
        }

        public List<HexTile> Adjacent { get; set; }
        public Vector3 Center { get; set; }
        public int Col { get; set; }
        public int Cost { get; set; }
        public Object Current { get; set; }
        public int Height { get; set; }
        public TileController Parent { get; set; }
        public int Row { get; set; }
    }
}
