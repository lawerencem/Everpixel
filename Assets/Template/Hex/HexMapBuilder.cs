﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Template.Hex
{
    public class HexMapBuilder
    {
        public static HexMap GetMap(int rows, int cols, float e, Vector3 seed)
        {
            var tiles = new List<HexTile>();

            for (int i = 0; i < cols; i += 2)
            {
                for (int j = 1; j < rows; j++)
                {
                    var xOffset = e * 0.75f;
                    var tile = new HexTile();
                    tile.SetCol(i);
                    tile.SetRow(j);
                    tile.SetCenter(new Vector3((seed.x + (xOffset * i)), (seed.y - (e * j)), seed.z));
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
                    tile.SetCol(i);
                    tile.SetRow(j);
                    tile.SetCenter(new Vector3((seed.x + (xOffset * i)), (ySeed.y - (e * j)), seed.z));
                    tiles.Add(tile);
                }
            }

            var map = new HexMap(tiles, rows, cols);
            PopulateAdjacentTiles(map);
            SetTilesParentMap(map);
            return map;
        }

        private static void PopulateAdjacentTiles(HexMap map)
        {
            foreach(var tile in map.Tiles)
            {
                var N = map.GetN(tile);
                var NE = map.GetNE(tile);
                var SE = map.GetSE(tile);
                var S = map.GetS(tile);
                var SW = map.GetSW(tile);
                var NW = map.GetNW(tile);

                if (N != null) { tile.SetN(N); }
                if (NE != null) { tile.SetNE(NE); }
                if (SE != null) { tile.SetSE(SE); }
                if (S != null) { tile.SetS(S); }
                if (SW != null) { tile.SetSW(SW); }
                if (NW != null) { tile.SetNW(NW); }
            }
        }

        private static void SetTilesParentMap(HexMap map)
        {
            foreach(var tile in map.Tiles)
                tile.SetParentMap(map);
        }
    }
}
