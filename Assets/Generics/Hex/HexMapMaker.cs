using Generics.Hex;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Generics.Hex
{
    public class HexMapMaker
    {
        public static GenericHexMap GetMap(int rows, int cols, float e, Vector3 seed)
        {
            var tiles = new List<HexTile>();

            for (int i = 0; i < cols; i += 2)
            {
                for (int j = 1; j < rows; j++)
                {
                    var xOffset = e * 0.75f;
                    var tile = new HexTile();
                    tile.Col = i;
                    tile.Row = j;
                    tile.Center = new Vector3((seed.x + (xOffset * i)), (seed.y - (e * j)), seed.z);
                    tiles.Add(tile);
                }
            }

            var h = e * 0.50f;
            var ySeed = new Vector3((seed.x + (e * 0.75f)), (seed.y - h), seed.z);

            for (int i = 1; i < cols; i += 2)
            {
                for (int j = 1; j < rows; j++)
                {
                    var xOffset = e * 0.75f;
                    var tile = new HexTile();
                    tile.Col = i;
                    tile.Row = j;
                    tile.Center = new Vector3((seed.x + (xOffset * i)), (ySeed.y - (e * j)), seed.z);
                    tiles.Add(tile);
                }
            }

            var map = new GenericHexMap(tiles, rows, cols);
            PopulateAdjacentTiles(map);
            return map;
        }

        private static void PopulateAdjacentTiles(GenericHexMap map)
        {
            foreach(var tile in map.Tiles)
            {
                var N = map.GetN(tile);
                var NE = map.GetNE(tile);
                var SE = map.GetSE(tile);
                var S = map.GetS(tile);
                var SW = map.GetSW(tile);
                var NW = map.GetNW(tile);

                if (N != null) { tile.Adjacent.Add(N); }
                if (NE != null) { tile.Adjacent.Add(NE); }
                if (SE != null) { tile.Adjacent.Add(SE); }
                if (S != null) { tile.Adjacent.Add(S); }
                if (SW != null) { tile.Adjacent.Add(SW); }
                if (NW != null) { tile.Adjacent.Add(NW); }
            }
        }
    }
}
