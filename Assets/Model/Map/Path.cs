using Assets.Controller.Map.Tile;
using System.Collections.Generic;

namespace Assets.Model.Map
{
    public class Path
    {
        public int Score { get; set; }
        public List<MTile> Tiles { get; set; }

        public Path()
        {
            this.Score = 0;
            this.Tiles = new List<MTile>();
        }

        public TileController GetNextTile(TileController t)
        {
            //if (this.Tiles.Contains(t.Model))
            //{
            //    int index = this.Tiles.IndexOf(t.Model) + 1;
            //    if (this.Tiles.Count > index)
            //        return this.Tiles[index].Parent;
            //}
            return null;
        }

        public void AddTile(MTile t)
        {
            if (this.Tiles.Count > 0)
                this.Score += (this.Tiles[this.Tiles.Count - 1].GetTravelCost(t));
            else
                this.Score += t.GetCost();
            this.Tiles.Add(t);
        }

        public Path DeepCopy()
        {
            var path = new Path();
            path.Score = this.Score;
            var newTiles = new List<MTile>();
            foreach (var tile in this.Tiles)
                newTiles.Add(tile);
            path.Tiles = newTiles;
            return path;
        }
    }
}
