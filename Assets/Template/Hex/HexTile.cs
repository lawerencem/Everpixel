using Assets.Template.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Template.Hex
{
    public class HexTile : IHex<HexTile> 
    {
        private HexTile _n;
        private HexTile _ne;
        private HexTile _se;
        private HexTile _s;
        private HexTile _sw;
        private HexTile _nw;

        private List<HexTile> _adjacent;
        private Vector3 _center;
        private int _col;
        private int _cost;
        private object _parentContainer;
        private HexMap _parentMap;
        private int _row;
        
        public Vector3 Center { get { return this._center; } }
        public int Col { get { return this._col; } }
        public int Cost { get { return this._cost; } }
        public object ParentContainer { get { return this._parentContainer; } }
        public int Row { get { return this._row; } }

        public HexTile()
        {
            this._adjacent = new List<HexTile>();
            this._cost = 0;
        }

        public void AddN(HexTile t) { this._adjacent.Add(t); this._n = t; }
        public void AddNE(HexTile t) { this._adjacent.Add(t); this._ne = t; }
        public void AddSE(HexTile t) { this._adjacent.Add(t); this._se = t; }
        public void AddS(HexTile t) { this._adjacent.Add(t); this._s = t; }
        public void AddSW(HexTile t) { this._adjacent.Add(t); this._sw = t; }
        public void AddNW(HexTile t) { this._adjacent.Add(t); this._nw = t; }

        public List<HexTile> GetAdjacent() { return this._adjacent; }
        public HexTile GetN() { return this._n; }
        public HexTile GetNE() { return this._ne; }
        public HexTile GetSE() { return this._se; }
        public HexTile GetS() { return this._s; }
        public HexTile GetSW() { return this._sw; }
        public HexTile GetNW() { return this._nw; }

        public void SetAdjacent(List<HexTile> a) { this._adjacent = a; }
        public void SetCenter(Vector3 c) { this._center = c; }
        public void SetCol(int col) { this._col = col; }
        public void SetCost(int cost) { this._cost = cost; }
        public void SetParentContainer(object o) { this._parentContainer = o; }
        public void SetParentMap(HexMap map) { this._parentMap = map; }
        public void SetRow(int row) { this._row = row; }

        public HexTile GetRandomNearbyTile(int probes)
        {
            var currNeighbors = this._adjacent;
            for(int i = 0; i < probes; i++)
            {
                var tile = ListUtil<HexTile>.GetRandomListElement(currNeighbors);
                currNeighbors = tile._adjacent;
            }
            return ListUtil<HexTile>.GetRandomListElement(currNeighbors); 
        }

        public List<HexTile> GetAoETiles(int dist)
        {
            var tiles = new List<HexTile>() { this };

            var closedSet = new List<HexTile>();
            var probeSet = new List<HexTile>() { this };
            var waitingSet = new List<HexTile>() { };

            for (int i = dist; i > 0; i--)
            {
                foreach (var tile in probeSet)
                {
                    foreach (var neighbor in tile.GetAdjacent())
                    {
                        var found = closedSet.Find(x => x.Col == neighbor.Col && x.Row == neighbor.Row);
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

        public List<HexTile> GetRaycastTiles(HexTile t, int dist)
        {
            var list = new List<HexTile>();

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

        public bool IsTileN(HexTile target, int dist)
        {
            var cur = this;
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

        public bool IsTileNE(HexTile target, int dist)
        {
            var cur = this;
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

        public bool IsTileSE(HexTile target, int dist)
        {
            var cur = this;
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

        public bool IsTileS(HexTile target, int dist)
        {
            var cur = this;
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

        public bool IsTileSW(HexTile target, int dist)
        {
            var cur = this;
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

        public bool IsTileNW(HexTile target, int dist)
        {
            var cur = this;
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


        public List<HexTile> GetRayTilesViaDistN(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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

        public List<HexTile> GetRayTilesViaDistNE(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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

        public List<HexTile> GetRayTilesViaDistSE(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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

        public List<HexTile> GetRayTilesViaDistS(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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

        public List<HexTile> GetRayTilesViaDistSW(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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

        public List<HexTile> GetRayTilesViaDistNW(HexTile t, int dist)
        {
            var list = new List<HexTile>();
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
