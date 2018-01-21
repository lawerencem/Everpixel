using Assets.Controller.Map.Tile;
using Assets.Template.Hex;
using System;
using System.Collections.Generic;

namespace Assets.Model.Map.Combat.Tile
{
    public class MTile : HexTile
    {
        private CTile _controller;
        private int _height;
        private MMap _map;

        public CTile Controller { get { return this._controller; } }
        public int Height { get { return this._height; } }
        public MMap Map { get { return this._map; } }

        public MTile() : base()
        {
            this._height = 1;
        }

        public MTile(HexTile tile)
        {
            this._height = 1;
            this._col = tile.GetCol();
            this._row = tile.GetRow();
            this._adjacent = tile.GetAdjacent();
        }

        public void SetN(MTile t) { this._adjacent.Add(t); this._n = t; }
        public void SetNE(MTile t) { this._adjacent.Add(t); this._ne = t; }
        public void SetSE(MTile t) { this._adjacent.Add(t); this._se = t; }
        public void SetS(MTile t) { this._adjacent.Add(t); this._s = t; }
        public void SetSW(MTile t) { this._adjacent.Add(t); this._sw = t; }
        public void SetNW(MTile t) { this._adjacent.Add(t); this._nw = t; }

        public void SetController(CTile c) { this._controller = c; }
        public void SetHeight(int h) { this._height = h; }
        public void SetMap(MMap m) { this._map = m; }

        public new MTile GetN() { return base.GetN() as MTile; }
        public new MTile GetNE() { return base.GetNE() as MTile; }
        public new MTile GetSE() { return base.GetSE() as MTile; }
        public new MTile GetS() { return base.GetS() as MTile; }
        public new MTile GetSW() { return base.GetSW() as MTile; }
        public new MTile GetNW() { return base.GetNW() as MTile; }

        public new List<MTile> GetAdjacent()
        {
            return this.ConvertIHexTiles(base.GetAdjacent());
        }

        public new List<MTile> GetAoETiles(int dist)
        {
            return this.ConvertIHexTiles(base.GetAoETiles(dist));
        }

        public new MTile GetRandomNearbyTile(int probes)
        {
            return base.GetRandomNearbyTile(probes) as MTile;
        }

        public int GetTravelCost(MTile tile)
        {
            var adjacent = this.GetAdjacent().Find(x => x.Equals(tile));
            if (adjacent != null)
            {
                double cost = tile.GetCost();
                int heightDiff = Math.Abs(this._height - tile.Height);
                if (heightDiff > 0)
                    cost *= (heightDiff + 0.5);
                return (int)Math.Ceiling(cost);
            }   
            else
                return int.MaxValue; // Sentinel value
        }

        private List<MTile> ConvertIHexTiles(List<IHex> tiles)
        {
            var mTiles = new List<MTile>();
            foreach (var tile in tiles)
                mTiles.Add(tile as MTile);
            return mTiles;
        }
    }
}
