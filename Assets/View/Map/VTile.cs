using Assets.Model.Map.Combat.Tile;
using UnityEngine;

namespace Assets.View.Map
{
    public class VTile
    {
        private Vector3 _center;
        private int _col;
        private int _row;

        public Vector3 Center { get { return this._center; } }
        public int Col { get { return this._col; } }
        public int Row { get { return this._row; } }

        public VTile(MTile h)
        {
            this._center = h.Center;
            this._col = h.GetCol();
            this._row = h.GetRow(); 
        }

        public Sprite Sprite { get; set; }
    }
}
