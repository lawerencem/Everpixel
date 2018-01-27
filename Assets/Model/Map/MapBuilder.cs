using Assets.Model.Map.Combat.Tile;
using Assets.Template.Hex;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Map
{
    public class MapBuilder
    {
        public HexMap GetMap(int rows, int cols, float e, Vector3 seed)
        {
            var tiles = new List<IHex>();

            for (int i = 0; i < cols; i += 2)
            {
                for (int j = 1; j < rows; j++)
                {
                    var xOffset = e * 0.75f;
                    var tile = new MTile();
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
                    var tile = new MTile();
                    tile.SetCol(i);
                    tile.SetRow(j);
                    tile.SetCenter(new Vector3((seed.x + (xOffset * i)), (ySeed.y - (e * j)), seed.z));
                    tiles.Add(tile);
                }
            }

            var map = new HexMap(tiles, rows, cols);
            this.PopulateAdjacentTiles(map);
            return map;
        }

        private void PopulateAdjacentTiles(HexMap map)
        {
            foreach(MTile tile in map.Tiles)
            {
                var N = (MTile) map.GetN(tile);
                var NE = (MTile) map.GetNE(tile);
                var SE = (MTile) map.GetSE(tile);
                var S = (MTile) map.GetS(tile);
                var SW = (MTile) map.GetSW(tile);
                var NW = (MTile) map.GetNW(tile);

                if (N != null) { tile.SetN(N); }
                if (NE != null) { tile.SetNE(NE); }
                if (SE != null) { tile.SetSE(SE); }
                if (S != null) { tile.SetS(S); }
                if (SW != null) { tile.SetSW(SW); }
                if (NW != null) { tile.SetNW(NW); }
            }
        }
    }
}
