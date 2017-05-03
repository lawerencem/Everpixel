using Controller.Map;
using Generics.Hex;
using Generics.Utilities;
using Model.Map;
using System.Collections.Generic;

namespace Model.Map
{
    public class Path
    {
        public Path()
        {
            this.Score = 0;
            this.Tiles = new List<HexTile>();
        }

        public int Score { get; set; }
        public List<HexTile> Tiles { get; set; }

        public void AddTile(HexTile t)
        {
            this.Tiles.Add(t);
            this.Score += t.Cost;
        }

        public TileController GetFinal()
        {
            return this.Tiles[this.Tiles.Count - 1].Parent;
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
