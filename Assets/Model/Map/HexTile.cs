using Controller.Map;
using Generics.Utilities;
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
    }
}
