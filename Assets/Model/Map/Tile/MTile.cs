using Assets.Controller.Map.Tile;
using Assets.Model.Map.Combat;
using Assets.Template.Hex;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Map.Tile
{
    public class MTile : IHex
    {
        private MTile _n;
        private MTile _ne;
        private MTile _se;
        private MTile _s;
        private MTile _sw;
        private MTile _nw;

        private List<MTile> _adjacent;
        private Vector3 _center;
        private int _col;
        private int _cost;
        private CTile _controller;
        private object _currentOccupant;
        private FTile _flags;
        private int _height;
        private HexTileLogic _logic;
        private MMap _map;
        private int _row;

        public CTile Controller { get { return this._controller; } }
        public Vector3 Center { get { return this._center; } }
        public int Height { get { return this._height; } }
        public MMap Map { get { return this._map; } }

        public MTile()
        {
            this._adjacent = new List<MTile>();
            this._flags = new FTile();
            this._height = 1;
            this._logic = new HexTileLogic();
        }

        public void SetN(MTile t) { this._adjacent.Add(t); this._n = t; }
        public void SetNE(MTile t) { this._adjacent.Add(t); this._ne = t; }
        public void SetSE(MTile t) { this._adjacent.Add(t); this._se = t; }
        public void SetS(MTile t) { this._adjacent.Add(t); this._s = t; }
        public void SetSW(MTile t) { this._adjacent.Add(t); this._sw = t; }
        public void SetNW(MTile t) { this._adjacent.Add(t); this._nw = t; }

        public void SetCenter(Vector3 center) { this._center = center; }
        public void SetCol(int col) { this._col = col; }
        public void SetController(CTile c) { this._controller = c; }
        public void SetCurrentOccupant(object o) { this._currentOccupant = o; }
        public void SetHeight(int h) { this._height = h; }
        public void SetMap(MMap m) { this._map = m; }
        public void SetRow(int row) { this._row = row; }

        public MTile GetN() { return this._n; }
        public MTile GetNE() { return this._ne; }
        public MTile GetSE() { return this._se; }
        public MTile GetS() { return this._s; }
        public MTile GetSW() { return this._sw; }
        public MTile GetNW() { return this._nw; }

        public FTile GetFlags() { return this._flags; }

        public List<MTile> GetAoETiles(int dist)
        {
            return this.ConvertIHexToMTile(this._logic.GetAoETiles(dist, this));
        }

        public int GetCost()
        {
            return this._cost;
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


        public IHex GetRandomNearbyTile(int probes)
        {
            return this._logic.GetRandomNearbyTile(probes, this);
        }

        public int GetCol()
        {
            return this._col;
        }

        public int GetRow()
        {
            return this._row;
        }

        public object GetCurrentOccupant()
        {
            return this._currentOccupant;
        }

        public List<IHex> GetRaycastTiles(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistN(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistNE(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistSE(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistS(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistSW(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetRayTilesViaDistNW(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetN()
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetNE()
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetSE()
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetS()
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetSW()
        {
            throw new NotImplementedException();
        }

        IHex IHex.GetNW()
        {
            throw new NotImplementedException();
        }

        public bool IsTileN(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public bool IsTileNE(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public bool IsTileSE(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public bool IsTileS(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public bool IsTileSW(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public bool IsTileNW(IHex target, int dist)
        {
            throw new NotImplementedException();
        }

        public List<IHex> GetAdjacent()
        {
            return this._adjacent.ConvertAll(o => (IHex) o);
        }

        public List<MTile> GetAdjacentMTiles()
        {
            return this._adjacent;
        }

        List<IHex> IHex.GetAoETiles(int dist)
        {
            throw new NotImplementedException();
        }

        private List<MTile> ConvertIHexToMTile(List<IHex> hexes)
        {
            var mTiles = new List<MTile>();
            foreach (var hex in hexes)
                mTiles.Add(hex as MTile);
            return mTiles;
        }
    }
}
