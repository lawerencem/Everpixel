using Assets.Template.Util;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Template.Hex
{
    public class HexTile : IHex
    {
        protected HexTile _n;
        protected HexTile _ne;
        protected HexTile _se;
        protected HexTile _s;
        protected HexTile _sw;
        protected HexTile _nw;

        protected List<IHex> _adjacent;
        protected Vector3 _center;
        protected int _col;
        protected int _cost;
        protected HexMap _parentMap;
        protected int _row;
        
        public Vector3 Center { get { return this._center; } }

        public HexTile()
        {
            this._adjacent = new List<IHex>();
            this._cost = 0;
        }

        public void SetN(HexTile t) { this._adjacent.Add(t); this._n = t; }
        public void SetNE(HexTile t) { this._adjacent.Add(t); this._ne = t; }
        public void SetSE(HexTile t) { this._adjacent.Add(t); this._se = t; }
        public void SetS(HexTile t) { this._adjacent.Add(t); this._s = t; }
        public void SetSW(HexTile t) { this._adjacent.Add(t); this._sw = t; }
        public void SetNW(HexTile t) { this._adjacent.Add(t); this._nw = t; }

        public List<IHex> GetAdjacent() { return this._adjacent; }

        public IHex GetN() { return this._n; }
        public IHex GetNE() { return this._ne; }
        public IHex GetSE() { return this._se; }
        public IHex GetS() { return this._s; }
        public IHex GetSW() { return this._sw; }
        public IHex GetNW() { return this._nw; }

        public void SetAdjacent(List<IHex> a) { this._adjacent = a; }
        public void SetCenter(Vector3 c) { this._center = c; }
        public void SetCol(int col) { this._col = col; }
        public void SetCost(int cost) { this._cost = cost; }
        public void SetParentMap(HexMap map) { this._parentMap = map; }
        public void SetRow(int row) { this._row = row; }

        public int GetCost()
        {
            return this._cost;
        }

        public int GetCol()
        {
            return this._col;
        }

        public object GetCurrentObject()
        {
            throw new NotImplementedException();
        }

        public int GetRow()
        {
            return this._row;
        }

        public IHex GetRandomNearbyTile(int probes)
        {
            var currNeighbors = this._adjacent;
            for(int i = 0; i < probes; i++)
            {
                var tile = ListUtil<IHex>.GetRandomElement(currNeighbors);
                currNeighbors = tile.GetAdjacent();
            }
            return ListUtil<IHex>.GetRandomElement(currNeighbors); 
        }

        public List<IHex> GetAoETiles(int dist)
        {
            var tiles = new List<IHex>() { this };

            var closedSet = new List<IHex>();
            var probeSet = new List<IHex>() { this };
            var waitingSet = new List<IHex>() { };

            for (int i = dist; i > 0; i--)
            {
                foreach (var tile in probeSet)
                {
                    foreach (var neighbor in tile.GetAdjacent())
                    {
                        var found = closedSet.Find(
                            x => x.GetCol() == neighbor.GetCol() && x.GetRow() == neighbor.GetRow());
                        if (found == null)
                        {
                            waitingSet.Add(neighbor);
                            tiles.Add(neighbor);
                            closedSet.Add(neighbor);
                        }
                    }
                }
                probeSet.Clear();
                foreach (var tile in waitingSet) { probeSet.Add(tile); }
                waitingSet.Clear();
            }
            return tiles;
        }

        public List<IHex> GetRaycastTiles(IHex t, int dist)
        {
            var list = new List<IHex>();

            if (this.IsTileN(t, dist))
                list = this.GetRayTilesViaDistN(t, dist);
            else if (this.IsTileNE(t, dist))
                list = this.GetRayTilesViaDistNE(t, dist);
            else if (this.IsTileSE(t, dist))
                list = this.GetRayTilesViaDistSE(t, dist);
            else if (this.IsTileS(t, dist))
                list = this.GetRayTilesViaDistS(t, dist);
            else if (this.IsTileSW(t, dist))
                list = this.GetRayTilesViaDistSW(t, dist);
            else if (this.IsTileNW(t, dist))
                list = this.GetRayTilesViaDistNW(t, dist);

            return list;
        }

        public bool IsTileN(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetN();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileNE(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNE();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileSE(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSE();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileS(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetS();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileSW(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSW();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsTileNW(IHex target, int dist)
        {
            var cur = this as IHex;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNW();
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }


        public List<IHex> GetRayTilesViaDistN(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetN();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistNE(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNE();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistSE(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSE();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistS(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetS();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistSW(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetSW();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        public List<IHex> GetRayTilesViaDistNW(IHex t, int dist)
        {
            var list = new List<IHex>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = cur.GetNW();
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }
    }
}
