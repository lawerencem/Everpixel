using Assets.Generics;
using Controller.Map;
using Generics.Hex;
using Generics.Utilities;
using Model.Map;
using System.Collections.Generic;

namespace Model.Map
{
    public class Path
    {
        public int Score { get; set; }
        public List<HexTile> Tiles { get; set; }

        public Path()
        {
            this.Score = 0;
            this.Tiles = new List<HexTile>();
        }

        public TileController GetNextTile(TileController t)
        {
            if (this.Tiles.Contains(t.Model))
            {
                int index = this.Tiles.IndexOf(t.Model) + 1;
                if (this.Tiles.Count > index)
                    return this.Tiles[index].Parent;
            }
            return null;
        }

        public void AddTile(HexTile t)
        {
            this.Tiles.Add(t);
            this.Score += t.Cost;
        }

        public Path DeepCopy()
        {
            var path = new Path();
            path.Score = this.Score;
            var newTiles = new List<HexTile>();
            foreach (var tile in this.Tiles)
                newTiles.Add(tile);
            path.Tiles = newTiles;
            return path;
        }
    }
}
