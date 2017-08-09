using Assets.Model.Map;
using UnityEngine;

namespace Assets.View.Map
{
    public class VTile
    {
        public Vector3 Center { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }

        public VTile(MTile h)
        {
            this.Center = h.Center;
            this.Col = h.Col;
            this.Row = h.Row; 
        }

        public Sprite Sprite { get; set; }
    }
}
