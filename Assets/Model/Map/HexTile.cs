using Controller.Map;
using Generics.Hex;
using Generics.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Map
{
    public class HexTile
    {
        private GenericHexMap _parentMap;

        public HexTile()
        {
            this.Adjacent = new List<HexTile>();
            this.Cost = 3;
            this.Height = 1;
        }

        public void SetParentMap(GenericHexMap map) { this._parentMap = map; }

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
            var tiles = new List<HexTile>();

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

        public List<HexTile> GetLOSTiles(HexTile s, int dist)
        {
            var list = new List<HexTile>();

            if (this._parentMap.IsTileN(s, this))
                list = this.GetLOSTilesViaDistN(s, dist);
            else if (this._parentMap.IsTileNE(s, this))
                list = this.GetLOSTilesViaDistNE(s, dist);
            else if (this._parentMap.IsTileSE(s, this))
                list = this.GetLOSTilesViaDistSE(s, dist);
            else if (this._parentMap.IsTileS(s, this))
                list = this.GetLOSTilesViaDistS(s, dist);
            else if (this._parentMap.IsTileSW(s, this))
                list = this.GetLOSTilesViaDistSW(s, dist);
            else
                list = this.GetLOSTilesViaDistNW(s, dist);

            return list;
        }

        protected List<HexTile> GetLOSTilesViaDistN(HexTile t, int dist)
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

        protected List<HexTile> GetLOSTilesViaDistNE(HexTile t, int dist)
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

        protected List<HexTile> GetLOSTilesViaDistSE(HexTile t, int dist)
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

        protected List<HexTile> GetLOSTilesViaDistS(HexTile t, int dist)
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

        protected List<HexTile> GetLOSTilesViaDistSW(HexTile t, int dist)
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

        protected List<HexTile> GetLOSTilesViaDistNW(HexTile t, int dist)
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
