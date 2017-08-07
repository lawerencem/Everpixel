using Controller.Map;
using System.Collections.Generic;
using Template.Utility;
using UnityEngine;

namespace Template.Hex
{
    public class HexTile
    {
        private HexMap _parentMap;

        public HexTile()
        {
            this.Adjacent = new List<HexTile>();
            this.Cost = 3;
            this.Height = 1;
        }

        public void SetParentMap(HexMap map) { this._parentMap = map; }

        public List<HexTile> Adjacent { get; set; }
        public Vector3 Center { get; set; }
        public int Col { get; set; }
        public int Cost { get; set; }
        public object Current { get; set; }
        public int Height { get; set; }
        public TileController Parent { get; set; }
        public int Row { get; set; }

        public HexTile GetRandomNearbyTile(int probes)
        {
            var currNeighbors = this.Adjacent;
            for(int i = 0; i < probes; i++)
            {
                var tile = ListUtil<HexTile>.GetRandomListElement(currNeighbors);
                currNeighbors = tile.Adjacent;
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
                    foreach (var neighbor in tile.Adjacent)
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

            if (this._parentMap.IsTileN(t, this))
                list = this.GetRayTilesViaDistN(t, dist);
            else if (this._parentMap.IsTileNE(t, this))
                list = this.GetRayTilesViaDistNE(t, dist);
            else if (this._parentMap.IsTileSE(t, this))
                list = this.GetRayTilesViaDistSE(t, dist);
            else if (this._parentMap.IsTileS(t, this))
                list = this.GetRayTilesViaDistS(t, dist);
            else if (this._parentMap.IsTileSW(t, this))
                list = this.GetRayTilesViaDistSW(t, dist);
            else
                list = this.GetRayTilesViaDistNW(t, dist);

            return list;
        }

        public HexTile GetN() { return this._parentMap.GetN(this); }
        public HexTile GetNE() { return this._parentMap.GetNE(this); }
        public HexTile GetSE() { return this._parentMap.GetSE(this); }
        public HexTile GetS() { return this._parentMap.GetS(this); }
        public HexTile GetSW() { return this._parentMap.GetSW(this); }
        public HexTile GetNW() { return this._parentMap.GetNW(this); }

        public bool IsHexN(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetN(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsHexNE(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetNE(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsHexSE(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetSE(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsHexS(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetS(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsHexSW(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetSW(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }

        public bool IsHexNW(HexTile target, int dist)
        {
            var cur = this;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetNW(cur);
                if (next != null)
                {
                    cur = next;
                    if (cur.Equals(target))
                        return true;
                }
            }
            return false;
        }


        protected List<HexTile> GetRayTilesViaDistN(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetN(cur);
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        protected List<HexTile> GetRayTilesViaDistNE(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetNE(cur);
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        protected List<HexTile> GetRayTilesViaDistSE(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetSE(cur);
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        protected List<HexTile> GetRayTilesViaDistS(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetS(cur);
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        protected List<HexTile> GetRayTilesViaDistSW(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetSW(cur);
                if (next != null)
                {
                    cur = next;
                    list.Add(cur);
                }
            }
            return list;
        }

        protected List<HexTile> GetRayTilesViaDistNW(HexTile t, int dist)
        {
            var list = new List<HexTile>();
            var cur = t;
            for (int i = 0; i < dist; i++)
            {
                var next = this._parentMap.GetNW(cur);
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
