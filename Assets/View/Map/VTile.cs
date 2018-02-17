using Assets.Model.Map.Tile;
using UnityEngine;

namespace Assets.View.Map
{
    public class VTile
    {
        private Vector3 _center;
        private int _col;
        private int _height;
        private int _row;
        private ETile _type;

        public Vector3 Center { get { return this._center; } }
        public int Col { get { return this._col; } }
        public int Height { get { return this._height; } }
        public int Row { get { return this._row; } }
        public ETile Type { get { return this._type; } }

        public VTile(MTile h)
        {
            this._center = h.Center;
            this._col = h.GetCol();
            this._height = h.GetHeight();
            this._row = h.GetRow();
            this._type = h.Type;
        }

        public Sprite Sprite { get; set; }
    }
}
